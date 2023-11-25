using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace WorkoutBuilderAPI.Application.Domain;

public class WorkoutModel
{
//     [BsonId]
//     [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }
    public string Name { get; set; }    
    public List<ExerciseModel> Exercises { get; set; }
}
