using System;

namespace ProductService.Shared
{

    public class BaseEvent : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }

}
