using System;

namespace ProductService.Shared
{
    public class ProdactAdded : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string ImageSource { get; set; }
        public string ProductUrl { get; set; }
        public string Description { get; set; }
        public double CurrentPrice { get; set; }

    }


}
