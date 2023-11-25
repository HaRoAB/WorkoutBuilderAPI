using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Application.Infrastructure;

public class MongoDbRepo : IWorkoutRepository
{
    private readonly string connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
    private readonly string databaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME");
    private readonly string collectionName = Environment.GetEnvironmentVariable("MONGO_COLLECTION_NAME");
    private readonly IConfiguration _configuration;
    private readonly IMongoCollection<WorkoutModel> _workouts;
    public MongoDbRepo (IConfiguration configuration)
    {
        _configuration = configuration;
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _workouts = database.GetCollection<WorkoutModel>(collectionName);
    }

    public Task CreateWorkout(WorkoutModel workout)
    {
        _workouts.InsertOneAsync(workout);
        return Task.CompletedTask;
    }

    public Task DeleteWorkout(Guid id)
    {
        var filter = Builders<WorkoutModel>.Filter.Eq("Id", id);
        _workouts.DeleteOneAsync(filter);
        return Task.CompletedTask;
    }

    public Task<WorkoutModel> GetWorkout(Guid id)
    {
        var filter = Builders<WorkoutModel>.Filter.Eq("Id", id);
        var result =_workouts.Find(filter).FirstOrDefaultAsync();
        return result;
    }

    public Task<List<WorkoutModel>> GetWorkouts()
    {
        return _workouts.Find(workout => true).ToListAsync();
    }

    public Task UpdateWorkout(WorkoutModel workout)
    {
        try
        {
            var filter = Builders<WorkoutModel>.Filter.Eq("Id", workout.Id);
            var update = Builders<WorkoutModel>.Update
                .Set("Id", workout.Id)
                .Set("Name", workout.Name)
                .Set("Exercises", workout.Exercises);
            _workouts.UpdateOne(filter, update);
            return Task.CompletedTask;
        }
        catch
        {
            Console.WriteLine("Request fails at db repository");
            return Task.CompletedTask;
        }
    }
}
