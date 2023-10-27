using WorkoutBuilderAPI.Models;

namespace WorkoutBuilderAPI.Interfaces;

public interface IWorkoutService
{
    Task<WorkoutDTO> GetWorkout(string id);
    Task<List<WorkoutDTO>> GetWorkouts();
    Task<WorkoutDTO> CreateWorkout(WorkoutDTO workout);
    Task<WorkoutDTO> UpdateWorkout(WorkoutDTO workout);
    Task<WorkoutDTO> DeleteWorkout(string id);
}