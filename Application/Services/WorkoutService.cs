using WorkoutBuilderAPI.Application.Interfaces;
using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Application.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutRepository _workoutRepository;

    public WorkoutService(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public Task CreateWorkout(WorkoutModel workout)
    {
        _workoutRepository.CreateWorkout(workout);
        return Task.CompletedTask;
    }

    public Task DeleteWorkout(Guid id)
    {
        _workoutRepository.DeleteWorkout(id);
        return Task.CompletedTask;
    }

    public Task<WorkoutModel> GetWorkout(Guid id)
    {
        return _workoutRepository.GetWorkout(id);

    }

    public Task<List<WorkoutModel>> GetWorkouts()
    {
        return _workoutRepository.GetWorkouts();
    }

    public Task UpdateWorkout(WorkoutModel workout)
    {
        _workoutRepository.UpdateWorkout(workout);
        return Task.CompletedTask;
    }
}