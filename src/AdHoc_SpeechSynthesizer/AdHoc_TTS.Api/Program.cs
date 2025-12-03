using AdHoc_SpeechSynthesizer.Common.Validation;
using AdHoc_SpeechSynthesizer.Dal.Ado;
using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Services;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

// https://localhost:7275/synthesize.html

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

await SeedDataAsync(app.Services);

ConfigureMiddleware(app, app.Environment);
ConfigureEndPoints(app);

app.Run();

// ================= Configuration Methods =============================

// Add Services to container
void ConfigureServices(IServiceCollection services,
                       IConfiguration configuration,
                       IHostEnvironment env)
{
    services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddXmlDataContractSerializerFormatters();

    services.AddRouting(options => options.LowercaseUrls = true);

    services.AddOpenApiDocument(settings =>
        settings.Title = "AdHoc Speech Synthesizer API");

    // DbContexts
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    services.AddDbContext<CompanyDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("CompanyConnection")));

    // Validator
    services.AddSingleton<SsmlValidator>();

    // DAOs
    services.AddScoped<IMessageTemplateDao, MessageTemplateDao>();
    services.AddScoped<ITtsModelDao, TtsModelDao>();
    services.AddScoped<ITtsVoiceDao, TtsVoiceDao>();

    services.AddScoped<ILocationDao, LocationDao>();
    services.AddScoped<IRouteDao, RouteDao>();
    services.AddScoped<IPlatformDao, PlatformDao>();
    services.AddScoped<ITargetTextDao, TargetTextDao>();

    // Services
    services.AddScoped<ITtsModelService, TtsModelService>();
    services.AddScoped<IMessageTemplateService, MessageTemplateService>();
    services.AddScoped<ITtsVoiceService, TtsVoiceService>();

    services.AddScoped<IRouteService, RouteService>();
    services.AddScoped<ILocationService, LocationService>();
    services.AddScoped<IPlatformService, PlatformService>();
    services.AddScoped<ITargetTextService, TargetTextService>();

    services.AddScoped<ISynthesisService, SynthesisService>();
}

// Configure the HTTP request pipeline
void ConfigureMiddleware(IApplicationBuilder app, IHostEnvironment env)
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseStaticFiles();

    app.UseOpenApi();
    app.UseSwaggerUi(settings => settings.Path = "/swagger");
}

// Configure routing system
void ConfigureEndPoints(IEndpointRouteBuilder app)
{
    app.MapControllers();
}

// ================= Seed Data =============================

async Task SeedDataAsync(IServiceProvider services)
{
    await TtsModelSeeder.SeedAsync(services);
    await TtsVoiceSeeder.SeedAsync(services);
    await MessageTemplateSeeder.SeedAsync(services);
}
