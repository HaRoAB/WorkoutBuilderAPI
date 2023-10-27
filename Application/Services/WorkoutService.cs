using WorkoutBuilderAPI.Application.Models;
using WorkoutBuilderAPI.Application.Interfaces;
using WorkoutBuilderAPI.Application.Domain;
using System.Text.Json;

namespace WorkoutBuilderAPI.Application.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutRepository _workoutRepository;

    public WorkoutService(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public Task CreateWorkout(string workoutJson)
    {
        WorkoutModel workout = JsonSerializer.Deserialize<WorkoutModel>(workoutJson);
        _workoutRepository.CreateWorkout(workout);
        return Task.CompletedTask;
    }

    public Task DeleteWorkout(string id)
    {
        _workoutRepository.DeleteWorkout(id);
        return Task.CompletedTask;
    }

    public Task<WorkoutModel> GetWorkout(string id)
    {
        return _workoutRepository.GetWorkout(id);
    
    }

    public Task<List<WorkoutModel>> GetWorkouts()
    {
        return _workoutRepository.GetWorkouts();
    }

    public Task UpdateWorkout(string workout)
    {
        WorkoutModel updatedWorkout = JsonSerializer.Deserialize<WorkoutModel>(workout);
        _workoutRepository.UpdateWorkout(updatedWorkout);
        return Task.CompletedTask;
    }
}