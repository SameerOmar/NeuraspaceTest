// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Excerya">
//      Author: Sameer Omar
//      Copyright (c) Excerya. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NeuraspaceTest;
using NeuraspaceTest.Contracts.Services;
using NeuraspaceTest.DataAccess;
using NeuraspaceTest.Helper;
using NeuraspaceTest.Models;
using NeuraspaceTest.Models.DataTransferModels;
using NeuraspaceTest.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NeuraspaceTest", Version = "v1" });
    c.OperationFilter<CustomSwaggerFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Register Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddSerilog(logger);

// Register Services
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseNpgsql("name=ConnectionStrings:DbContext",
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddScoped<ICollisionEventService<CollisionEventData, CollisionEvent>, CollisionEventService>();
builder.Services.AddScoped<IOperatorService<OperatorData, Operator>, OperatorService>();
builder.Services.AddScoped<ISatelliteService<SatelliteData, Satellite>, SatelliteService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    DbInitializer.Initialize(dbContext);
}

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