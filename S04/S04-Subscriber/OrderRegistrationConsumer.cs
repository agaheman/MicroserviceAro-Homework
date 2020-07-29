using MassTransit;
using S04_Contracts;
using System;
using System.Threading.Tasks;

namespace S04_Subscriber
{
    public class OrderRegistrationConsumer : IConsumer<OrderRegistered>
    {
        public async Task Consume(ConsumeContext<OrderRegistered> context)
        {
            System.Threading.Thread.Sleep(10);

            if (context.Message.OrderId == 2)
            {
                var orderRejectedMessage = new OrderRejected
                {
                    RejectBy = "Alireza Oroumand rejector",
                    Reason = "nothing else matter",
                    RejectDate = DateTime.Now,
                    OrderId = context.Message.OrderId

                };

                await context.RespondAsync(orderRejectedMessage);
            }
            else
            {
                var orderAcceptedMessage = new OrderAccepted
                {
                    AcceptBy = "Alireza Oroumand",
                    AcceptDate = DateTime.Now,
                    OrderId = context.Message.OrderId

                };

                await context.RespondAsync(orderAcceptedMessage);
            }
        }
    }
}
