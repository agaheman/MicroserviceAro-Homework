using MassTransit;
using S04_Contracts;
using System;
using System.Threading.Tasks;

namespace S04_Publisher
{
    class Program
    {
        static void Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://localhost", hostConfigurator =>
                {
                    hostConfigurator.Username("admin");
                    hostConfigurator.Password("admin");
                });

                cfg.OverrideDefaultBusEndpointQueueName("OrderRegistration-PublisherExchange");
            });

            bus.Start();
            var orderRegisteredMessage = new OrderRegistered
            {
                OrderId = 1,
                OrderDate = DateTime.Now,
                CustomerNumber = DateTime.Now.Millisecond.ToString(),
            };

            var requestClient = bus.CreateRequestClient<OrderRegistered>(RequestTimeout.After(s: 5));
            var response = requestClient.GetResponse<OrderAccepted, OrderRejected>(orderRegisteredMessage).Result;

            //DO Some thing with response.
            Console.ReadKey();

            bus.Stop();
        }
    }
}
