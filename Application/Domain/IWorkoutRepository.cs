namespace WorkoutBuilderAPI.Application.Domain
{
    public interface IWorkoutRepository
    {
        Task<WorkoutModel> GetWorkout(string id);
        Task<List<WorkoutModel>> GetWorkouts();
        Task CreateWorkout(WorkoutModel workout);
        Task UpdateWorkout(WorkoutModel workout);
        Task DeleteWorkout(string id);   
    }
}