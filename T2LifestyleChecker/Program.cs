using Microsoft.OpenApi.Models;
using T2LifestyleChecker.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "T2 Lifestyle Checker API",
        Version = "1.0.0" 
    });
});

//Load Resources
builder.Configuration.AddJsonFile("Resources/Messages.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("Resources/ScoringRules.json", optional: false, reloadOnChange: true);
// Add Libraries
builder.Services.AddLibraries(builder.Configuration);

builder.Services.AddHttpClient(); // This registers IHttpClientFactory

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
