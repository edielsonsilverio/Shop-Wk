using MediatR;

namespace Shop.MessageBus.Messages;

public abstract class Message : IRequest
{
    public string MessageType { get; protected set; }

    public Message()
    {
        MessageType = GetType().Name;
    }
}
