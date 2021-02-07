using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using ProductService.Shared;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ParserService.Models;

namespace ParserService.Controllers
{
    public class ParseController : Controller
    {
        private readonly ILogger<ParseController> _logger;
        IMongoCollection<ParseProdactHistory> _prodactHistory;
        private readonly IEventBus _eventBus;

        public ParseController(ILogger<ParseController> logger, IEventBus eventBus)
        {
            _eventBus = eventBus;
            _logger = logger;
            var client = new MongoClient("mongodb://mongo:27017");
            var database = client.GetDatabase("ParserService");
            _prodactHistory = database.GetCollection<ParseProdactHistory>("ParseProdactHistory");
        }

        [HttpPost]
        public async Task<ParsedProdactDto> Parse(ParsedProdactDto item)
        {
            _eventBus.Publish(new ProdactAdded
            {
                Id = new Guid(),
                Name = item.Name,
                CreationTime = DateTime.Now,
                Description = item.Description,
                Code = item.Code,
                ImageSource = item.ImageSource,
                ProductUrl = item.ProductUrl,
                CurrentPrice = item.Price
            });

            AddParseProdactHistory(item);
            return item;
        }

        public void AddParseProdactHistory(ParsedProdactDto item)
        {

            _prodactHistory.InsertOne(
            new ParseProdactHistory()
            {
                Name = item.Name,
                CreationTime = DateTime.Now,
                Description = item.Description,
                Code = item.Code,
                ImageSource = item.ImageSource,
                ProductUrl = item.ProductUrl,
                Price = item.Price

            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
