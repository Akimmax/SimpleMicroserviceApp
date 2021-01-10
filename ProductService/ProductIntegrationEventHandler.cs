using MassTransit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using ProductService.Shared;
using ProductService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{
    public class ProductPriceChangedIntegrationEventHandler
    {
        IMongoCollection<Product> _products;
        IMongoCollection<Supplement> _supplement;
        IProductRepository _productRepository;
        public ProductPriceChangedIntegrationEventHandler(IProductRepository productRepository)
        {

            _productRepository = productRepository;
            var client = new MongoClient("mongodb://mongo:27017");
            var database = client.GetDatabase("ProductService");
            _products = database.GetCollection<Product>("Products");
            _supplement = database.GetCollection<Supplement>("Logs");

        }

        public async Task Handle(ProdactAdded @event)
        {
            var prodact = new Product()
            {
                Name = @event.Name,
                CreationTime = @event.CreationTime,
                Code = @event.Code,
                Description = @event.Description,
                ImageSource = @event.ImageSource,
                ProductUrl = @event.ProductUrl,
                Prices = new List<Price>() { new Price() { CurrentPrice = @event.CurrentPrice } }
            };

            _productRepository.Create(prodact);
        }
        public async Task Handle(ProdactUpdated @event)
        {
            var old = _productRepository.Get(@event.Code);
            old.Prices.Add(new Price() { CurrentPrice = @event.NewPrice });

            var prodact = new Product()
            {
                Name = @event.Name,
                CreationTime = @event.UpdateTime,
                Code = @event.Code,
                Description = @event.Description,
                ImageSource = @event.ImageSource,
                ProductUrl = @event.ProductUrl,
                Prices = old.Prices
            };

            _productRepository.Create(prodact);
        }

        public async Task LogException(Exception ex)
        {
            _supplement.InsertOne(new Supplement() { SupplementName = $"Exception: {ex.Message}" });
        }


    }

}
