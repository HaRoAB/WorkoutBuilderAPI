using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace WorkoutBuilderAPI.Application.Domain
{
    public class WorkoutModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Workout_Id { get; set; } = string.Empty;

        public string Name { get; set; } 
        public List<ExerciseModel> Exercises { get; set; }
    }
}