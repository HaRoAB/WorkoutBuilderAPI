using System.Text.Json;
using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Application.Infrastructure;

public class JsonRepository : IWorkoutRepository
{
    private readonly string  directoryPath = "JSON";    

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
        string workoutJson = File.ReadAllText($"{directoryPath}/{workoutName}.json");
        WorkoutModel workout = JsonSerializer.Deserialize<WorkoutModel>(workoutJson);
        return Task.FromResult(workout);
    }

    public Task<List<WorkoutModel>> GetWorkouts()
    {
        string[] workoutFiles = Directory.GetFiles(directoryPath);
        List<WorkoutModel> workouts = new List<WorkoutModel>();
        foreach (string file in workoutFiles)
        {
            string workoutJson = File.ReadAllText(file);
            WorkoutModel workout = JsonSerializer.Deserialize<WorkoutModel>(workoutJson);
            workouts.Add(workout);
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