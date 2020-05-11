using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventHubSender
{
    class Program
    {
        private const string connectionString = "Endpoint=sb://tsgeventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=1g3xlCgyatyVfqcjayOeTGcSN56IHPofXBHrdjovyTs=";
        private const string eventHubName = "myeventhub";
        static async Task Main()
        {
            // Create a producer client that you can use to send events to an event hub
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                // Create a batch of events 
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                Random r = new Random();
                // Add events to the batch. An event is a represented by a collection of bytes and metadata. 
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(r.Next() + " year " + DateTime.Now.Year)));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(r.Next() + " year " + DateTime.Now.Year)));
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(r.Next() + " year " + DateTime.Now.Year)));

                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("A batch of 3 events has been published.");
            }

        }
    }
}