using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Application.Infrastructure
{
    public class MongoDbRepo : IWorkoutRepository
    {
        private readonly string connectionString;
        private readonly string databaseName;
        private readonly string collectionName;

        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<WorkoutModel> _workouts;

        public MongoDbRepo(IConfiguration configuration)
        {
            // Attempt to get values from environment variables
            connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
            databaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME");
            collectionName = Environment.GetEnvironmentVariable("MONGO_COLLECTION_NAME");

            // If any of the environment variables is null, use default values
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(collectionName))
            {
                connectionString = "mongodb+srv://Hannsis:lollipop123@cluster0.wvu1dqq.mongodb.net/";
                databaseName = "all_your_database_are_belong_to_us";
                collectionName = "workouts";

                // Optionally, log a message indicating that default values are used
                Console.WriteLine("Env var are null, default values used.");
            }
            else
            {
                // Log the values retrieved from environment variables
                Console.WriteLine("ConnectionString: " + connectionString);
                Console.WriteLine("DatabaseName: " + databaseName);
                Console.WriteLine("CollectionName: " + collectionName);
            }

            _configuration = configuration;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _workouts = database.GetCollection<WorkoutModel>(collectionName);
        }


        public Task CreateWorkout(WorkoutModel workout)
        {
            try
            {
                Console.WriteLine("Attempting to create workout in the database.");
                Console.WriteLine($"Logging time: {DateTime.Now}");

                _workouts.InsertOneAsync(workout);
                Console.WriteLine($"workout: {workout}");

                Console.WriteLine("Workout created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating workout: {ex.Message}");
            }

            return Task.CompletedTask;
        }

        public Task DeleteWorkout(Guid id)
        {
            try
            {
                Console.WriteLine($"Attempting to delete workout with ID: {id}");

                var filter = Builders<WorkoutModel>.Filter.Eq("Id", id);
                _workouts.DeleteOneAsync(filter);

                Console.WriteLine("Workout deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting workout: {ex.Message}");
            }

            return Task.CompletedTask;
        }
        public Task<WorkoutModel> GetWorkout(Guid id)
        {
            try
            {
                Console.WriteLine($"Attempting to retrieve workout with ID: {id}");

                var filter = Builders<WorkoutModel>.Filter.Eq("Id", id);
                var result = _workouts.Find(filter).FirstOrDefaultAsync();

                Console.WriteLine("Workout retrieved successfully.");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving workout: {ex.Message}");
                return null;
            }
        }

        public Task<List<WorkoutModel>> GetWorkouts()
        {
            try
            {
                Console.WriteLine("Attempting to retrieve all workouts.");
                Console.WriteLine($"Logging time: {DateTime.Now}");

                return _workouts.Find(workout => true).ToListAsync();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving workouts: {ex.Message}");
                Console.WriteLine($"Logging time: {DateTime.Now}");
                return null;
            }
        }
        public Task UpdateWorkout(WorkoutModel workout)
        {
            try
            {
                Console.WriteLine($"Attempting to update workout with ID: {workout.Id}");

                var filter = Builders<WorkoutModel>.Filter.Eq("Id", workout.Id);
                var update = Builders<WorkoutModel>.Update
                    .Set("Id", workout.Id)
                    .Set("Name", workout.Name)
                    .Set("Exercises", workout.Exercises);
                _workouts.UpdateOne(filter, update);

                Console.WriteLine("Workout updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating workout: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
