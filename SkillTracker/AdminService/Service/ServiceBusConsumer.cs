using AdminService.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminService.Service
{
    public class ServiceBusConsumer : IServiceBusConsumer
    {
        private const string QUEUE_NAME = "skillprofilequeue";
        private readonly ServiceBusClient _client;
        private ServiceBusProcessor _processor;
        private readonly ILogger<ServiceBusConsumer> _logger;

        public ServiceBusConsumer(IConfiguration configuration, ILogger<ServiceBusConsumer> logger)
        {
            var connectionString = configuration.GetConnectionString("ServiceBus");
            _client = new ServiceBusClient(connectionString);
            _logger = logger;
        }

        public async Task CloseQueueAsync()
        {
            if (_processor != null)
            {
                await _processor.CloseAsync().ConfigureAwait(false);
            }

        }

        public async ValueTask DisposeQueuAsync()
        {
            if(_processor != null)
            {
                await _processor.DisposeAsync().ConfigureAwait(false);
            }

            if (_client != null)
            {
                await _client.DisposeAsync().ConfigureAwait(false);
            }
        }

        public async Task ReceiveMessages()
        {
            ServiceBusProcessorOptions serviceBusProcessorOptions = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false
            };

            _processor = _client.CreateProcessor(QUEUE_NAME, serviceBusProcessorOptions);
            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;
            await _processor.StartProcessingAsync().ConfigureAwait(false);
        }

        private Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var content = Encoding.UTF8.GetString(args.Message.Body.ToArray());
            var addedProfile = JsonConvert.DeserializeObject<SkillProfile>(content);
            _logger.LogDebug("Profile added", addedProfile);
            return Task.CompletedTask;
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, "Message handler encountered an eeception");
            return Task.CompletedTask;
        }
    }
}
