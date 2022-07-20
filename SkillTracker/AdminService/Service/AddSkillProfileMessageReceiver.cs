using AdminService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminService.Service
{
    public class AddSkillProfileMessageReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private string _queueName;

        public AddSkillProfileMessageReceiver(IOptions<RabbitMqConfiguration> rabbitMqConfiguration, IConfiguration configuration)
        {
            try
            {
                ConnectionFactory factory;
                var rabbitMqConfig = rabbitMqConfiguration.Value;
                var rabbitclient = Environment.GetEnvironmentVariable("rabbit_mq");
                if (rabbitclient != null)
                {
                    factory = new ConnectionFactory
                    {
                        Uri = new Uri(rabbitclient)
                    };

                }
                else
                {
                    factory = new ConnectionFactory
                    {
                        HostName = rabbitMqConfig.HostName,
                        UserName = rabbitMqConfig.UserName,
                        Password = rabbitMqConfig.Password
                    };
                }

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _queueName = rabbitMqConfig.QueueName;
                _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("rabbitclient: " + Environment.GetEnvironmentVariable("rabbit_mq"));
                Console.WriteLine(ex.Message);
                throw (ex);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var addedProfile = JsonConvert.DeserializeObject<SkillProfile>(content);
                Console.WriteLine("New associate " + addedProfile.AssociateInfo.AssociateId + " skill added");
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += (ch, ea) => { };
            consumer.Unregistered += (ch, ea) => { };
            consumer.ConsumerCancelled += (ch, ea) => { };
            consumer.Registered += (ch, ea) => { };

            _channel.BasicConsume(_queueName, false, consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
