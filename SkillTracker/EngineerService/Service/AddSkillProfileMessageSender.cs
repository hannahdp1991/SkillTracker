using EngineerService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Text;

namespace EngineerService.Service
{
    public class AddSkillProfileMessageSender : IAddSkillProfileMessageSender
    {
        private string _hostName;
        private string _queueName;
        private string _userName;
        private string _password;
        private IConnection _connection;
        private IConfiguration _configuration;

        public AddSkillProfileMessageSender(IOptions<RabbitMqConfiguration> rabbitMqConfig, IConfiguration configuration)
        {
            _hostName = rabbitMqConfig.Value.HostName;
            _queueName = rabbitMqConfig.Value.QueueName;
            _userName = rabbitMqConfig.Value.UserName;
            _password = rabbitMqConfig.Value.Password;
            _configuration = configuration;
            CreateConnection();
        }

        public void SendSkillProfile(SkillProfile profile)
        {
            if (hasConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var json = JsonConvert.SerializeObject(profile);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                }
            }
        }

        private bool hasConnection()
        {
            if (_connection == null)
            {
                CreateConnection();
            }

            return _connection != null;
        }

        private void CreateConnection()
        {
            try
            {
                var rabbitclient = Environment.GetEnvironmentVariable("rabbit_mq");
                ConnectionFactory factory;

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
                        HostName = _hostName,
                        UserName = _userName,
                        Password = _password
                    };
                }

                _connection = factory.CreateConnection();
                Console.WriteLine(_connection);
                Console.WriteLine("Connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine("rabbitclient: " + Environment.GetEnvironmentVariable("rabbit_mq"));
                Console.WriteLine(ex.Message);
                throw (ex);
            }
        }
    }
}
