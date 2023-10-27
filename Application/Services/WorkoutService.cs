using WorkoutBuilderAPI.Models;
using WorkoutBuilderAPI.Interfaces;
using System.Text.Json;

namespace WorkoutBuilderAPI.Services;

public class WorkoutService : IWorkoutService
{
    public Task<WorkoutDTO> CreateWorkout(WorkoutDTO workout)
    {
        // create JSON file with workout data
        string json = JsonSerializer.Serialize(workout);
        File.WriteAllTextAsync($"{workout.Name}.json", json);
        return Task.FromResult(workout);
    }

    public Task<WorkoutDTO> DeleteWorkout(string id)
    {
        throw new NotImplementedException();
    }

    public Task<WorkoutDTO> GetWorkout(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<WorkoutDTO>> GetWorkouts()
    {
        throw new NotImplementedException();
    }

    public Task<WorkoutDTO> UpdateWorkout(WorkoutDTO workout)
    {
        throw new NotImplementedException();
    }
}