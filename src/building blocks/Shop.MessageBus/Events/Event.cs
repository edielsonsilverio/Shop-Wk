namespace Shop.MessageBus.Events;

public abstract class Event
{
    public DateTime Timespam { get; protected set; }

    public Event()
    {
        Timespam = DateTime.Now;
    }
}
