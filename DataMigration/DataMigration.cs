using MongoDB.Bson;
using MongoDB.Driver;

namespace WorkoutBuilderAPI.DataMigration;
public class DataMigration
{
    public static void MigrateDataToMongoDB()
    {
        string connectionString = Environment.GetEnvironmentVariable("MONOGODB_CONNECTION_STRING");
    
        MongoClient client = new (connectionString);

        IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME"));
        IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(Environment.GetEnvironmentVariable("MONGO_COLLECTION_NAME"));

        // Read data from a JSON file
        string jsonFilePath = "C:/Users/89hanmad/source/repos/Examesnarbete/WorkoutBuilderAPI/Application/Infrastructure/JSON/workout.json";
        string jsonData = File.ReadAllText(jsonFilePath);

        // Parse JSON data into BsonDocument objects
        var documents = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument[]>(jsonData);

        // Insert the documents into the collection
        collection.InsertMany(documents);

        Console.WriteLine("Data migrated successfully!");
    }
}

//how to add a loop to check if data already is there, as it is now it will migrate data that exists already
