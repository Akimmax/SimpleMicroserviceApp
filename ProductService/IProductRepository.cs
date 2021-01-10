using MongoDB.Driver;
using ProductService.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService
{


    public interface IProductRepository
    {

        public IEnumerable<Product> GetAll();
        public void Create(Product item);
        public void Update(Product item);
        public Product Get(string code);
        public Product Delete(string code);

        public void CreateAll(IEnumerable<Product> items);


       
    }
}
