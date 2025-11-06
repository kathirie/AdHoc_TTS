using AdHoc_SpeechSynthesizer.Data;
using AdHoc_SpeechSynthesizer.Services;
using AdHoc_SpeechSynthesizer.Services.AppContext;
using AdHoc_SpeechSynthesizer.Services.CompanyContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces;
using AdHoc_SpeechSynthesizer.Services.Interfaces.AppContext;
using AdHoc_SpeechSynthesizer.Services.Interfaces.CompanyContext;

using AdHoc_SpeechSynthesizer.Services.Synthesizers;
using Microsoft.EntityFrameworkCore;
using System.Speech.Synthesis;

// https://localhost:7275/synthesize.html

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<CompanyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CompanyConnection")));

builder.Services.AddScoped<ITtsModelService, TtsModelService>();
builder.Services.AddScoped<ITtsVoiceService, TtsVoiceService>();
builder.Services.AddScoped<IMessageTemplateService, MessageTemplateService>();

builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<ITargetTextService, TargetTextService>();

builder.Services.AddScoped<ISynthesisService, SynthesisService>();

var app = builder.Build();

// Seed the voices *before* the app starts listening

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
