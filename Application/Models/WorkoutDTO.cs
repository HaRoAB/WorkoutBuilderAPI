
namespace WorkoutBuilderAPI.Models;

public class WorkoutDTO
{

    public string Id { get; set; }
    public string Name { get; set; }
    public List<ExerciseDTO> Exercises { get; set; }
}