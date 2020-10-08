using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intro1
{
    internal static class Program
    {
        private const string QueueName = "test";

        private const string ServiceBusConnectionString =
                    "";

        private static IQueueClient _queueClient;

        // Use this handler to examine the exceptions received on the message pump.
        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception{exceptionReceivedEventArgs.Exception}.");
            ExceptionReceivedContext exceptionReceivedContext = exceptionReceivedEventArgs.ExceptionReceivedContext; 
            Console.WriteLine("Exception context for troubleshooting:"); 
            Console.WriteLine($"- Endpoint: {exceptionReceivedContext.Endpoint}"); 
            Console.WriteLine($"- Entity Path: {exceptionReceivedContext.EntityPath}"); 
            Console.WriteLine($"- Executing Action: {exceptionReceivedContext.Action}"); 
            return Task.CompletedTask;
        }

        private static async Task Main(string[] args)
        {
            _queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            RegisterOnMessageHandlerAndReceiveMessages();

            Console.ReadKey();
            await _queueClient.CloseAsync();
        }

        private static async Task ProcessMessageAsync(Message message, CancellationToken cancellationToken)
        {
            /* Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed. If queueClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc. to avoid unnecessary exceptions.*/

            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber}Body:{Encoding.UTF8.GetString(message.Body)}");

            /* Complete the message so that it is not received again.
             This can be done only if the queue Client is created in ReceiveMode.PeekLock mode (which is the default). */
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
            };

            _queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
        }
    }
}