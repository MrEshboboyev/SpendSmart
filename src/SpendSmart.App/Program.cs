using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SpendSmart.App.Extensions;
using SpendSmart.App.Middleware; // Ensure the namespace for the middleware is included
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()); // Ensure logs are written to the console

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddSwaggerGen();

// Register the LogContextEnrichmentMiddleware
builder.Services.AddTransient<LogContextEnrichmentMiddleware>();

// Configure app configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());
}

// Configure services
builder.Services.InstallServicesFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Log application startup
Log.Information("Starting application...");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the LogContextEnrichmentMiddleware
app.UseMiddleware<LogContextEnrichmentMiddleware>();

app.UseSerilogRequestLogging();
app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(configure => configure.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Log application shutdown
Log.Information("Application is shutting down...");
