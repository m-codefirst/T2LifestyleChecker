using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using T2LifestyleChecker.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region CORS Configuration

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000") // React frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
});

#endregion CORS Configuration

#region Configuration Files
//Part Three(Optional / Advanced)
//How could the code be implemented in such a way that the scoring mechanism could be altered without requiring the code to be recompiled and re-deployed? This could be a change to age groups or scores for individual questions.
builder.Configuration
    .AddJsonFile("Resources/Messages.json", optional: false, reloadOnChange: true)
    .AddJsonFile("Resources/ScoringRules.json", optional: false, reloadOnChange: true);

#endregion Configuration Files

#region Service Registrations

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "T2 Lifestyle Checker API",
        Version = "1.0.0"
    });
});
builder.Services.AddLibraries(builder.Configuration); // Custom DI registrations
builder.Services.AddHttpClient(); // Register IHttpClientFactory

var app = builder.Build();

#endregion Service Registrations

#region Middleware Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();

#endregion Middleware Pipeline
