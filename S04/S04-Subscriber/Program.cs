using MassTransit;
using System;
using System.Threading.Tasks;

namespace S04_Subscriber
{
    class Program
    {
        static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://localhost", hostConfigurator =>
                {
                    hostConfigurator.Username("admin");
                    hostConfigurator.Password("admin");
                });

                cfg.ReceiveEndpoint("OrderRegistration-Subscriber", e =>
                {
                    e.Consumer<OrderRegistrationConsumer>();
                });
            });

            await busControl.StartAsync();
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
