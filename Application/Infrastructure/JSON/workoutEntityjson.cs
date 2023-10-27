
using System.Text.Json.Serialization;

namespace WorkoutBuilderAPI.Application.JSON;
public class ReceptEntityJson
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("reps")]
    public string Reps { get; set; }

    [JsonPropertyName("sets")]
    public string Sets { get; set; }
    
    [JsonPropertyName("rest")]
    public string Rest { get; set; }

}
