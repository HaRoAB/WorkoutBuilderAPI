using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Application.Interfaces;

public interface IWorkoutService
{
    Task<WorkoutModel> GetWorkout(string id);
    Task<List<WorkoutModel>> GetWorkouts();
    Task CreateWorkout(string workout);
    Task UpdateWorkout(string workout);
    Task DeleteWorkout(string id);
}