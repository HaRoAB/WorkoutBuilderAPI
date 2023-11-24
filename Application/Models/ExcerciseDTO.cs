namespace WorkoutBuilderAPI.Application.Models;
public class ExerciseDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public TimeSpan Rest { get; set; }
    public float Weight { get; set; }
}
