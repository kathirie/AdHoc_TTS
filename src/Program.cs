using AdHoc_SpeechSynthesizer.Common.Validation;
using AdHoc_SpeechSynthesizer.Dal.Ado;
using AdHoc_SpeechSynthesizer.Dal.Interface;
using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Services;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using Microsoft.EntityFrameworkCore;

// https://localhost:7275/synthesize.html

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<CompanyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CompanyConnection")));

builder.Services.AddSingleton<SsmlValidator>();

// DAOs
builder.Services.AddScoped<IMessageTemplateDao, MessageTemplateDao>();
builder.Services.AddScoped<ITtsModelDao, TtsModelDao>();
builder.Services.AddScoped<ITtsVoiceDao, TtsVoiceDao>();

builder.Services.AddScoped<ILocationDao, LocationDao>();
builder.Services.AddScoped<IRouteDao, RouteDao>();
builder.Services.AddScoped<IPlatformDao, PlatformDao>();
builder.Services.AddScoped<ITargetTextDao, TargetTextDao>();


// Services
builder.Services.AddScoped<ITtsModelService, TtsModelService>();
builder.Services.AddScoped<IMessageTemplateService, MessageTemplateService>();
builder.Services.AddScoped<ITtsVoiceService, TtsVoiceService>();

builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<ITargetTextService, TargetTextService>();

builder.Services.AddScoped<ISynthesisService, SynthesisService>();

var app = builder.Build();

await TtsModelSeeder.SeedAsync(app.Services);
await TtsVoiceSeeder.SeedAsync(app.Services);
await MessageTemplateSeeder.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
