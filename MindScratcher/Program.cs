using ATI.Services.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindScratcher.Managers;
using MindScratcher.Models.Options;
using MindScratcher.Repositories;
using Newtonsoft.Json.Serialization;
using ConfigurationManager = ATI.Services.Common.Behaviors.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager.ConfigurationRoot = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureByName<MongoOptions>();

builder.Services.AddSingleton<CardRepository>();
builder.Services.AddSingleton<FolderRepository>();

builder.Services.AddSingleton<CardManager>();
builder.Services.AddSingleton<FolderManager>();

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