using Shop.MessageBus.Events;
using ShopServices.RabbitMQ.Bus.Commands;

namespace Shop.MessageBus.BusRabbit;

public interface IRabbitEventBus
{
    Task SendCommand<T>(T command) where T : Command;

    void Publish<T>(T @event) where T : Event;

    void Subscribe<T, TH>() where T : Event
                           where TH : IEventHandler<T>;
}
