using System;
using System.Collections.Generic;

namespace ProductService.Shared
{

    public class Product
    {
        public Product()
        {
            Prices = new List<Price>();
        }

        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string ImageSource { get; set; }
        public string ProductUrl { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Price> Prices { get; set; }

        }
    public class Price
    {
        public int ItemId { get; set; }
        public virtual Product Item { get; set; }
        public DateTimeOffset Date { get; set; }
        public double CurrentPrice { get; set; }
    }

}
