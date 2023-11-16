
namespace WorkoutBuilderAPI.Application.Domain;

public class ExerciseModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public TimeSpan? Rest { get; set; }
    public float Weight { get; set; }
}
