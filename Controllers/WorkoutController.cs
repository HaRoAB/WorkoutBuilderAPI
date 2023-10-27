using Microsoft.AspNetCore.Mvc;

namespace WorkoutBuilderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkoutController : ControllerBase 
{
    private readonly IWorkoutService _workoutService;

    

}