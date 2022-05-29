namespace Shop.MessageBus.BusRabbit;

public interface IRabbitMQConnection
{
    RabbitMQSettings GetConnectionString();
}

