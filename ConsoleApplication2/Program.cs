using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProdusserAp
{
    class Program
    {
        public static string GeneratorPin()
        {
            string pin = string.Empty;
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                pin += r.Next(0, 10);
            }
            return pin;
        }
        public static void ProductMassageToQueue(string message, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            //Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }
        static void Main(string[] args)
        {
            while (true)
            {
                ProductMassageToQueue("Hi", "messages");
                Thread.Sleep(3000);
            }
        }
    }
}
