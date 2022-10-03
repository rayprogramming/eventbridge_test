namespace RayProgramming.Google.Analytics.Events;

public class EventBody
{
    public string client_id { get; set; } = string.Empty;
    public string user_id { get; set; } = string.Empty;
    public List<Event> events { get; set; } = new List<Event>();
}