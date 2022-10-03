using System.Text.Json;
using Amazon.SecretsManager.Extensions.Caching;

namespace RayProgramming.Google.Analytics;
public class GoogleAnalyticSecrets
{
    public String? MeasurementId { get; set; }
    public String? ApiSecret { get; set; }

    private SecretsManagerCache cache = new SecretsManagerCache();
    private const String GoogleAnalyticsKey = "eventBridge/test/googleAnalytics";

    public async Task<GoogleAnalyticSecrets?> GetSecret()
    {
        String googleAnalyticsJson = await cache.GetSecretString(GoogleAnalyticsKey);
        GoogleAnalyticSecrets? gaSecrets = JsonSerializer.Deserialize<GoogleAnalyticSecrets>(googleAnalyticsJson);
        return gaSecrets;
    }
}