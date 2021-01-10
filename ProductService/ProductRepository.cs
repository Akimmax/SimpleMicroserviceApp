using MongoDB.Driver;
using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{
    public class ProductRepository: IProductRepository
    {
        IMongoCollection<Product> _products;
        public ProductRepository()
        {
            var client = new MongoClient("mongodb://mongo:27017");
            var database = client.GetDatabase("ProductService");
            _products = database.GetCollection<Product>("Products");
        }

        public IEnumerable<Product> GetAll()
        {
            var items = _products.Find(_ => true).ToList();
            return items;
        }

        public void Create(Product item)
        {
            _products.InsertOne(item);
        }

        public void Update(Product item)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Code, item.Code);

            var result = _products.ReplaceOne(filter, item);
        }

        public Product Get(string code)
        {
            var item = _products.Find(i => i.Code == code).FirstOrDefault();
            return item;
        }

        public void CreateAll(IEnumerable<Product> items)
        {
            var bulkOperations = new List<WriteModel<Product>>();
            foreach (var item in items)
            {
                var upsertOne = new ReplaceOneModel<Product>(
                    Builders<Product>.Filter.Where(x => x.Code == item.Code),
                    item);
                upsertOne.IsUpsert = true;
                bulkOperations.Add(upsertOne);
            }
            _products.BulkWrite(bulkOperations);
        }

        public Product Delete(string code)
        {
            throw new NotImplementedException();
        }
    }
}
