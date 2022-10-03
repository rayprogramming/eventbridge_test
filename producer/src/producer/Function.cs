using Amazon.Lambda.Core;
using Amazon.EventBridge.Model;
using System.Text.Json;
using RayProgramming.Google.Analytics.Events;
using Amazon.EventBridge;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace producer;

public class Function
{

    private AmazonEventBridgeClient client = new AmazonEventBridgeClient();    
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<PutEventsResponse> FunctionHandler(string input, ILambdaContext context)
    {
        PutEventsRequest transactionEvents = new PutEventsRequest
        {
            Entries = {
                new PutEventsRequestEntry
                {
                    Source = "my.custom.app",
                    EventBusName = "test-event-bus",
                    DetailType = "Test",
                    Time = DateTime.Now,
                    Detail = JsonSerializer.Serialize(
                        new EventBody
                        {
                            client_id = input,
                            events = new List<Event>(){
                                new Event{
                                    name = "Producer Test",
                                    parameters = new Dictionary<string, string>(){
                                        {"Parameter", "1"}
                                    }
                                }
                            }

                        }
                    )
                }
            }
        };
        var response = await client.PutEventsAsync(transactionEvents);
        return response;   
    }
}
