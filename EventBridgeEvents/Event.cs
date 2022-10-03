using System.Text.Json.Serialization;

namespace RayProgramming.Google.Analytics.Events;
public class Event
{
    public string name { get; set; } = string.Empty;

    [JsonPropertyName("params")]
    public Dictionary<string, string> parameters = new Dictionary<string, string>();
}