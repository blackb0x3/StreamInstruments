using Serilog;
using StreamInstruments.DataAccess.Services;
using StreamInstruments.Hubs.Api.Data;
using StreamInstruments.Hubs.Api.Domain.Infrastructure;
using StreamInstruments.Hubs.Api.Infrastructure.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
DataAccessInstaller.Install(builder.Services);
DomainInstaller.Install(builder.Services);
InfrastructureInstaller.Install(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();