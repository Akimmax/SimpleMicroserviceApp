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
using ProductService.Models;

namespace ProductService.Controllers
{

    public class ProductController : Controller
    {
        private readonly IBus _bus;
        private readonly IProductRepository _productRepository;
        public ProductController(IBus bus, IProductRepository productRepository)
        {
            _bus = bus;
            _productRepository = productRepository;
        }

        public async Task<string> Test()
        {
            return "ServController 6";
        }

        [HttpGet("/api/products/")]
        public ActionResult<List<Product>> Get()
        {
            return _productRepository.GetAll().ToList();
        }


        [HttpGet("/api/products/{code}")]
        public ActionResult<Product> Get(string code)
        {
            return _productRepository.Get(code);
        }

        [HttpPost("/api/products")]
        public ActionResult<Product> AddProduct(Product product)
        {
            _productRepository.Create(product);
            return product;
        }

        [HttpPut("/api/products")]
        public ActionResult<Product> UpdateProduct(Product product)
        {   
            product.Name = product.Name + " upd";
            _productRepository.Create(product);
            return new Product() { Name = "UpdateProduct" };
        }

        [HttpDelete("/api/products/{code}")]
        public ActionResult<string> DeleteProduct(string code)
        {
            _productRepository.Delete(code);
            return code;
        }



    }


}
