using Azure.Messaging.ServiceBus;
using EngineerService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerService.Service
{
    public class ServiceBusSender
    {
        private readonly ServiceBusClient _client;
        private readonly Azure.Messaging.ServiceBus.ServiceBusSender _clientSender;
        private const string QUEUE_NAME = "skillprofilequeue";

        public ServiceBusSender(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ServiceBus");
            _client = new ServiceBusClient(connectionString);
            _clientSender = _client.CreateSender(QUEUE_NAME);
        }

        public async Task SendMessage(SkillProfile profile)
        {
            var json = JsonConvert.SerializeObject(profile);
            var body = Encoding.UTF8.GetBytes(json);

            ServiceBusMessage message = new ServiceBusMessage(body);
            await _clientSender.SendMessageAsync(message).ConfigureAwait(false);
        } 

    }
}
