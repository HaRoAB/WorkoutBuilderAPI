namespace WorkoutBuilderAPI.Application.Domain
{
    public interface IWorkoutRepository
    {
        Task<WorkoutModel> GetWorkout(Guid id);
        Task<List<WorkoutModel>> GetWorkouts();
        Task CreateWorkout(WorkoutModel workout);
        Task UpdateWorkout(WorkoutModel workout);
        Task DeleteWorkout(Guid id);   
    }
}