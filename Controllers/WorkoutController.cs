using WorkoutBuilderAPI.Application.Models;
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
    public Task GetWorkout(string id)
    {
        var workout = _workoutService.GetWorkout(id);
        return Task.FromResult(workout);
    }

    [HttpPost]
    public Task CreateWorkout(string workout)
    {
        _workoutService.CreateWorkout(workout);
        return Task.CompletedTask;
    }

    [HttpPut]
    public Task UpdateWorkout(string workout)
    {
        _workoutService.UpdateWorkout(workout);
        return Task.CompletedTask;
    }

    [HttpDelete("{id}")]
    public Task DeleteWorkout(string id)
    {
        _workoutService.DeleteWorkout(id);
        return Task.CompletedTask;
    }

}