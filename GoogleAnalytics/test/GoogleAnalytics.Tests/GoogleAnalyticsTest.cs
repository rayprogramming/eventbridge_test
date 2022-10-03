using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.CloudWatchEvents;
using RayProgramming.Google.Analytics;
using RayProgramming.Google.Analytics.Events;

namespace GoogleAnalyticsTest.Tests;

public class GoogleAnalyticsTest
{
    [Fact]
    public async void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var googleAnalytics = new GoogleAnalytics();
        var context = new TestLambdaContext();
        var eventPayload = new CloudWatchEvent<EventBody>()
        {
            Id = "cdc73f9d-aea9-11e3-9d5a-835b769c0d9c",
            DetailType = "Scheduled Event",
            Source = "aws.events",
            Account = "",
            Time = Convert.ToDateTime("1970-01-01T00:00:00Z"),
            Region = "us-west-2",
            Resources = new List<string>() { "arn:aws:events:us-west-2:123456789012:rule/ExampleRule" },
            Detail = new EventBody{
                client_id = "asdf"
            } 
        };

        await googleAnalytics.SendEvent(eventPayload, context);

        Assert.Equal("Done", "Done");
    }
}
