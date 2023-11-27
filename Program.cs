using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using WorkoutBuilderAPI.Application.Domain;
using WorkoutBuilderAPI.Application.Infrastructure;
using WorkoutBuilderAPI.Application.Interfaces;
using WorkoutBuilderAPI.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWorkoutService, WorkoutService>();
//builder.Services.AddScoped<IWorkoutRepository, JsonRepository>();


//DataMigration.MigrateDataToMongoDB();
// builder.Services.AddScoped<IWorkoutRepository>(_ =>
// {
//     var connectionString = "mongodb+srv://Hannsis:lollipop123@cluster0.wvu1dqq.mongodb.net/";
//     var mongoClient = new MongoClient(connectionString);
    
//     return new MongoDbRepo(mongoClient, "all_your_database_are_belong_to_us");
// });

var connectionString = "mongodb+srv://Hannsis:lollipop123@cluster0.wvu1dqq.mongodb.net/";
var databaseName = "all_your_database_are_belong_to_us"; 

builder.Services.AddScoped<IMongoClient>(_ => new MongoClient(connectionString));
builder.Services.AddScoped<IWorkoutRepository>(_ => new MongoDbRepo(
    _.GetService<IMongoClient>(),
    databaseName
));

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
