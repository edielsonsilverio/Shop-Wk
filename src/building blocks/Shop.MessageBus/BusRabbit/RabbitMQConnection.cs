namespace Shop.MessageBus.BusRabbit;

public class RabbitMQConnection : IRabbitMQConnection
{
    private readonly RabbitMQSettings _connection;

    public RabbitMQConnection(RabbitMQSettings connection)
    {
        _connection = connection;
    }

    public RabbitMQSettings GetConnectionString()
    {
        return _connection;
    }
}

