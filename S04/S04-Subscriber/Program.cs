using MassTransit;
using System;
using System.Threading.Tasks;

namespace S04_Subscriber
{
    class Program
    {
        static async void Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://localhost", hostConfigurator =>
                {
                    hostConfigurator.Username("guest");
                    hostConfigurator.Password("guest");
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
