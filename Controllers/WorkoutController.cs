using Microsoft.AspNetCore.Mvc;
using WorkoutBuilderAPI.Application.Interfaces;
using System.Text.Json;
using WorkoutBuilderAPI.Application.Domain;

namespace WorkoutBuilderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService _workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkouts()
    {
        var workouts = await _workoutService.GetWorkouts();
        var json = JsonSerializer.Serialize(workouts);

        return Content(json, "application/json");
    }


    [HttpGet("{id}")]
    public Task GetWorkout(Guid id)
    {
        var workout = _workoutService.GetWorkout(id);
        return Task.FromResult(workout);
    }

    [HttpPost]
    public Task CreateWorkout(WorkoutModel workout)
    {
        _workoutService.CreateWorkout(workout);
        return Task.CompletedTask;
    }

    [HttpPut]
    public Task UpdateWorkout(WorkoutModel workout)
    {
        _workoutService.UpdateWorkout(workout);
        return Task.CompletedTask;
    }

    [HttpDelete("{id}")]
    public Task DeleteWorkout(Guid id)
    {
        _workoutService.DeleteWorkout(id);
        return Task.CompletedTask;
    }

}