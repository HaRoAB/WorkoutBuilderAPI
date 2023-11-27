using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Application.Interfaces;

public interface IWorkoutService
{
    Task<WorkoutModel> GetWorkout(Guid id);
    Task<List<WorkoutModel>> GetWorkouts();
    Task CreateWorkout(WorkoutModel workout);
    Task UpdateWorkout(WorkoutModel workout);
    Task DeleteWorkout(Guid id);
}