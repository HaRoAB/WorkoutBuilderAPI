using Microsoft.AspNetCore.HttpOverrides;
using WorkoutBuilderAPI.Application.Domain;
using WorkoutBuilderAPI.Application.Infrastructure;
using WorkoutBuilderAPI.Application.Interfaces;
using WorkoutBuilderAPI.Application.Services;
using WorkoutBuilderAPI.DataMigration;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IWorkoutRepository, MongoDbRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});


var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "";
if (!string.IsNullOrEmpty(port))
{
    app.Urls.Add($"http://*:{port}");
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
