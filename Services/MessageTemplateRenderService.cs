using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Helpers;
using AdHoc_SpeechSynthesizer.Helpers.Validation;
using AdHoc_SpeechSynthesizer.Models.Requests;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdHoc_SpeechSynthesizer.Services
{
    public class MessageTemplateRenderService : IMessageTemplateRenderService
    {
        private readonly AppDbContext _appDb;
        private readonly SsmlValidator _validator;

        public MessageTemplateRenderService(AppDbContext appDb, SsmlValidator validator)
        {
            _appDb = appDb;
            _validator = validator;
        }

        public async Task<string> RenderSsmlAsync(SynthesizeFromTemplateRequest request)
        {
            // 1) Load template from AppDbContext by GUID
            var template = await _appDb.MessageTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TemplateId == request.TemplateId);

            if (template == null)
                throw new InvalidOperationException($"MessageTemplate with id '{request.TemplateId}' not found.");

            // 2) Build placeholder → value dictionary
            var values = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(request.RefLocationName))
                values["Location.RefLocationName"] = request.RefLocationName;

            if (!string.IsNullOrWhiteSpace(request.PlatformName))
                values["Platform.Name"] = request.PlatformName;

            if (request.RouteNr.HasValue)
                values["Route.RouteNr"] = request.RouteNr.Value.ToString();

            if (!string.IsNullOrWhiteSpace(request.FrontText))
                values["TargetText.FrontText"] = request.FrontText;

            if (!string.IsNullOrWhiteSpace(request.FreeText))
                values["Freitext"] = request.FreeText;

            // 3) Render placeholders into SSMLContent
            var renderedSsml = TemplateRenderer.Render(template.SsmlContent, values);

            // 4) Validate final SSML
            var validation = _validator.Validate(renderedSsml);
            if (!validation.IsXmlWellFormed || !validation.IsSchemaValid)
            {
                var errors = string.Join(Environment.NewLine, validation.Errors);
                throw new InvalidOperationException(
                    $"Rendered SSML is not valid.\n{errors}");
            }

            return renderedSsml;
        }
    }
}
