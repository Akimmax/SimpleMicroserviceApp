using ProductService.Shared;

namespace ParserService
{
    public interface IEventBus
    {
        void Publish(ProdactAdded @event);
        void Publish(ProdactUpdated @event);

    }
}
