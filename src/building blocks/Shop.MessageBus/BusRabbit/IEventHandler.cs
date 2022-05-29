using Shop.MessageBus.Events;

namespace Shop.MessageBus.BusRabbit;

public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
{
    Task Handle(TEvent @event);
}

public interface IEventHandler
{

}

