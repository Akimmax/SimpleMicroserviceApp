using MassTransit;
using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParserService
{
    public class EventBus: IEventBus
    {
        private readonly IBus _bus;
        private readonly Uri uri = new Uri("rabbitmq://host.docker.internal/ticketQueue");


        public EventBus(IBus bus)
        {
            _bus = bus;
        }
        public async void Publish(ProdactAdded @event)
        {

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(@event);
        }
        public async void Publish(ProdactUpdated @event)
        {

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(@event);
        }
    }
}
