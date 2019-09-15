using MassTransit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using CommunicationModel;
using DatabaseModel;
using Newtonsoft.Json;

namespace Receiver
{
    public class Receiver
    {
        private static string QUEUE_NAME = "new-order-queue";

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("RECEIVER has started. \nPress any key to stop.");
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
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var jsonString = Encoding.UTF8.GetString(body);
                    NewOrderMessage newOrderMessage = JsonConvert.DeserializeObject<NewOrderMessage>(jsonString);
                    Console.WriteLine($"Received{jsonString}");
                    using (var context = new FoodServiceContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                context.Order.Add(newOrderMessage.NewOrder);
                                context.OrderCustomer.Add(newOrderMessage.NewOrderCustomer);
                                context.OrderEmployee.Add(newOrderMessage.NewOrderEmployee);
                                context.OrderItem.AddRange(newOrderMessage.NewOrderItems);

                                context.SaveChanges();
                                transaction.Commit();

                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();
                            }

                        }
                    }

                };
                try
                {
                    channel.BasicConsume(QUEUE_NAME, false, consumer);
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
