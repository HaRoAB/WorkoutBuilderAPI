using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Application.Infrastructure;

public class MongoDbRepo : IWorkoutRepository
{

    private readonly IMongoCollection<WorkoutModel> _collection;

    public MongoDbRepo(IMongoClient client, string databaseName)
    {
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<WorkoutModel>("DataMigration");
    }

    public async Task CreateWorkout(WorkoutModel workout)
    {
        await _collection.InsertOneAsync(workout);
    }

    public async Task DeleteWorkout(string Id)
    {
        var filter = Builders<WorkoutModel>.Filter.Eq("_id", Id);
        await _collection.DeleteOneAsync(filter);
    }
    public async Task<WorkoutModel> GetWorkout(string Id)
    {
        var filter = Builders<WorkoutModel>.Filter.Eq("_id", Id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }


    public async Task UpdateWorkout(WorkoutModel updatedWorkout)
    {
        var filter = Builders<WorkoutModel>.Filter.Eq("_id", updatedWorkout.Id);
        var update = Builders<WorkoutModel>.Update
            .Set("Name", updatedWorkout.Name)
            .Set("Exercises", updatedWorkout.Exercises);

        await _collection.UpdateOneAsync(filter, update);
    }


    public async Task<List<WorkoutModel>> GetWorkouts()
    {
        var workouts = await _collection.Find(_ => true).ToListAsync();
        return workouts;
    }
}