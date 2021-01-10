using MassTransit;
using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{
    public class ProductEventConsumer :
        IConsumer<ProdactAdded>,
        IConsumer<ProdactUpdated>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductPriceChangedIntegrationEventHandler _handler;

        public ProductEventConsumer(IServiceProvider serviceProvider, IProductRepository productRepository)
        {
            _serviceProvider = serviceProvider;
            _handler = new ProductPriceChangedIntegrationEventHandler(productRepository);
        }
        public async Task Consume(ConsumeContext<ProdactAdded> context)
        {
            var evt = context.Message;
            await _handler.Handle(evt);
        }

        public async Task Consume(ConsumeContext<ProdactUpdated> context)
        {
            var evt = context.Message;
            await _handler.Handle(evt);
        }

    }
}
