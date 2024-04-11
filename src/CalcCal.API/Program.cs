using CalcCal.API.Extensions;
using Carter;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.InjectDependencies(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseCors("DefaultPolicy");

if (app.Environment.IsProduction()) app.UseHttpsRedirection();

app.UseRouting();

app.AddHealthChecks();

app.AddMiddlewares();

if(app.Environment.IsProduction()) app.SecureApp();

app.UseAuthentication();

app.UseAuthorization();

if (app.Environment.IsProduction())
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

app.MapCarter();

if (app.Environment.IsProduction()) app.MapFallbackToController("Index", "Fallback");

await app.RunAsync();