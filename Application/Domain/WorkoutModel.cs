namespace WorkoutBuilderAPI.Application.Domain
{
    public class WorkoutModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ExerciseModel> Exercises { get; set; }
    }
}