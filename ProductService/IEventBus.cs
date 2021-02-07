using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{
    public interface IEventBus
    {
        void Publish(IEvent @event);

    }
}
