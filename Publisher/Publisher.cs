using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommunicationModel;
using DatabaseModel;
using MassTransit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Publisher
{
    public class Publisher
    {
        public void Publish(NewOrderMessage newOrderMessage)
        {
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(QUEUE_NAME, false, false, false, null);
                var consumer = new EventingBasicConsumer(channel);
                try
                {
                    string jsonString = JsonConvert.SerializeObject(newOrderMessage);
                    var body = Encoding.UTF8.GetBytes(jsonString);
                    channel.BasicPublish("", QUEUE_NAME, null, body);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private static string QUEUE_NAME = "new-order-queue";

    }
}
