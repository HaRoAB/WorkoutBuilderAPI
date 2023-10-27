namespace WorkoutBuilderAPI.Domain
{
    public interface IWorkoutRepository
    {
        Task<WorkoutModel> GetWorkout(string id);
        Task<List<WorkoutModel>> GetWorkouts();
        Task<WorkoutModel> CreateWorkout(WorkoutModel workout);
        Task<WorkoutModel> UpdateWorkout(WorkoutModel workout);
        Task<WorkoutModel> DeleteWorkout(string id);   
    }
}