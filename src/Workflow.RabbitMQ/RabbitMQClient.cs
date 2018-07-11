using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Configuration;
using System.Text;
using Workflow.DataContract.Fingerprint;

namespace Workflow.RabbitMQ
{
    public class RabbitMQClient : IDisposable
    {
        protected string HOST_NAME = "10.60.0.113";
        protected string USER_NAME = "";
        protected string PASSWORD = "";
        protected string EXCHANGE_NAME = "K2 Exchange";
        protected string ROUTING_KEY = "client_route_key";
        protected string QUEUE_NAME = "";
        protected int PORT = -1;
        protected string EXCHANGE_TYPE = "fanout";

        protected IConnection connection;
        protected IModel channel;

        public delegate void OnRecievedHandler(IModel channel, string message);
        private OnRecievedHandler recieved;
        public event OnRecievedHandler OnRecieved
        {
            add
            {
                recieved -= value;
                recieved = null;
                recieved += value;
            }
            remove
            {
                recieved -= value;
            }
        }

        public RabbitMQClient()
        {
            Init();
        }

        public RabbitMQClient(string queueName, string routingKey = "")
        {
            if (!string.IsNullOrEmpty(routingKey))
                ROUTING_KEY = routingKey;

            if (!string.IsNullOrEmpty(queueName))
                QUEUE_NAME = queueName;

            Init();
        }

        public void Init()
        {
            LoadConfiguration();
            CreateConnection();
            RegisterConsummer();
        }

        public void LoadConfiguration()
        {
            var configuration = ConfigurationManager.AppSettings;

            if (!string.IsNullOrEmpty(configuration["RabbitMQHost"]))
                HOST_NAME = configuration["RabbitMQHost"];

            if (!string.IsNullOrEmpty(configuration["RabbitMQUser"]))
                USER_NAME = configuration["RabbitMQUser"];

            if (!string.IsNullOrEmpty(configuration["RabbitMQPassword"]))
                PASSWORD = configuration["RabbitMQPassword"];

            if (!string.IsNullOrEmpty(configuration["RabbitMQPort"]))
                PORT = int.Parse(configuration["RabbitMQPort"]);
        }

        protected void CreateConnection()
        {
            var cnnfactory = new ConnectionFactory() { HostName = HOST_NAME, UserName = USER_NAME, Password = PASSWORD, Port = PORT };
            connection = cnnfactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(QUEUE_NAME, true, false, false, null);
            channel.QueueBind(queue: QUEUE_NAME, exchange: EXCHANGE_NAME, routingKey: ROUTING_KEY);
        }

        public void CloseConnection()
        {
            if (connection != null && connection.IsOpen)
                connection.Close();
        }

        public void Publish(string message, bool persistent = true)
        {
            Publish(MessageCommandEnum.PUSH, message, persistent);
        }

        public void Publish(MessageCommandEnum messageCommand, string message, bool persistent = true)
        {
            var properties = channel.CreateBasicProperties();
            properties.Persistent = persistent;
            var messageBody = Encoding.UTF8.GetBytes(JsonSerialize(messageCommand, message));
            channel.BasicPublish(exchange: EXCHANGE_NAME, routingKey: ROUTING_KEY, basicProperties: properties, body: messageBody);
        }

        public void RegisterConsummer()
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var channel = sender as EventingBasicConsumer;
                var responseBody = e.Body;
                var message = Encoding.UTF8.GetString(responseBody);
                recieved(this.channel, message);
                channel.Model.BasicAck(e.DeliveryTag, false);
            };
            channel.BasicConsume(queue: QUEUE_NAME, noAck: false, consumer: consumer);
        }

        public string JsonSerialize(MessageCommandEnum command, object data)
        {
            CommandObject commandObject = new CommandObject()
            {
                Command = command,
                Data = data
            };
            var config = new JsonSerializerSettings();
            config.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return JsonConvert.SerializeObject(new {
                Command = command,
                Data = data
            }, config);
        }

        public CommandObject JsonDeserialize(string message)
        {
            var config = new JsonSerializerSettings();
            config.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return JsonConvert.DeserializeObject<CommandObject>(message, config);
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
