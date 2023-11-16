using System.Text.Json;
using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Application.Infrastructure;

public class JsonRepository : IWorkoutRepository
{
    private readonly string directoryPath = "./Application/Infrastructure/JSON";

    public Task CreateWorkout(WorkoutModel workout)
    {
        string json = JsonSerializer.Serialize(workout);
        File.WriteAllTextAsync($"{directoryPath}/{workout.Name}.json", json);
        return Task.CompletedTask;
    }

    public Task DeleteWorkout(string workoutName)
    {
        File.Delete($"{workoutName}.json");
        return Task.CompletedTask;
    }

    public Task<WorkoutModel> GetWorkout(string workoutName)
    {
        if (!File.Exists($"{directoryPath}/{workoutName}.json"))
        {
            throw new Exception("Workout does not exist");
        }
        else
        {
            string workoutJson = File.ReadAllText($"{directoryPath}/{workoutName}.json");
            WorkoutModel workout = JsonSerializer.Deserialize<WorkoutModel>(workoutJson);
            return Task.FromResult(workout);
        }
    }

    public Task<List<WorkoutModel>> GetWorkouts()
    {
        var workouts = new List<WorkoutModel>();
        var workoutFiles = Directory.GetFiles(directoryPath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // Ignore case during deserialization
            IgnoreNullValues = true, // Ignore null values during deserialization
                                     // Add more settings as needed
        };

        foreach (var file in workoutFiles)
        {
            try
            {
                string workoutJson = File.ReadAllText(file);
                var workout = JsonSerializer.Deserialize<WorkoutModel>(workoutJson, options);
                workouts.Add(workout);
            }
            catch (JsonException ex)
            {
                // Log or handle the deserialization exception
                Console.WriteLine($"Error deserializing file '{file}': {ex.Message}");
                // Log the JSON content causing the issue
                Console.WriteLine($"Problematic JSON content:\n{File.ReadAllText(file)}");
            }
        }

        return Task.FromResult(workouts);
    }


    public Task UpdateWorkout(WorkoutModel workout)
    {
        string workoutJson = JsonSerializer.Serialize(workout);
        File.WriteAllTextAsync($"{directoryPath}/{workout.Name}.json", workoutJson);
        return Task.CompletedTask;
    }
}