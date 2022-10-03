using System.Text;
using System.Text.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.CloudWatchEvents;
using RayProgramming.Google.Analytics.Events;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RayProgramming.Google.Analytics;
public class GoogleAnalytics
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string host = "www.google-analytics.com";
    private static readonly string path = "/mp/collect";
    private static readonly string contentType = "application/json";

    private string BuildUrl(GoogleAnalyticSecrets gaSecrets)
    {
        return $"https://{host}{path}?api_secret={gaSecrets.ApiSecret}&measurement_id={gaSecrets.MeasurementId}";
    }
    public async Task SendEvent(CloudWatchEvent<EventBody> eventBody, ILambdaContext context)
    {
        var s = new GoogleAnalyticSecrets();
        GoogleAnalyticSecrets? gaSecrets = await s.GetSecret();
        if (gaSecrets is null) throw new Exception("Cannot Retrieve Secrets");

        context.Logger.LogInformation(JsonSerializer.Serialize(eventBody.Detail));

        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(BuildUrl(gaSecrets)))
        {
            Content = new StringContent(JsonSerializer.Serialize(eventBody.Detail), Encoding.UTF8, contentType)
        };

        request.Headers.Accept.Clear();
        request.Headers.Add("Accept", "*/*");

        var response = await client.SendAsync(request);
    }
}