using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Text.Json;

public class RabbitMQService
{
    private readonly string _hostname;
    private readonly string _username;
    private readonly string _password;
    private readonly string _exchangeName;
    private readonly string _queueName = "kiemke";
    public RabbitMQService(IConfiguration configuration)
    {
        _hostname = configuration["RabbitMQ:HostName"];
        _username = configuration["RabbitMQ:UserName"];
        _password = configuration["RabbitMQ:Password"];
        _exchangeName = configuration["RabbitMQ:ExchangeName"];
        _queueName = configuration["RabbitMQ:QueueName"];
    }

    public async Task SendMessage<T>(T messageObject)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostname,
            UserName = _username,
            Password = _password
        };

        var _connection = await factory.CreateConnectionAsync();

        using (var channel = await _connection.CreateChannelAsync())
        {

            await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false,
    arguments: null);
            await channel.ExchangeDeclareAsync(
            exchange: _exchangeName,
            type: ExchangeType.Direct,  // hoặc "fanout", "topic", "headers"
            durable: false,
            autoDelete: false,
            arguments: null
            );
            await channel.QueueBindAsync(
                queue: _queueName,
                exchange: _exchangeName,
                routingKey: _queueName
            );
            string jsonMessage = JsonSerializer.Serialize(messageObject);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            await channel.BasicPublishAsync(exchange: _exchangeName, routingKey: _queueName, body: body);

        }
    }
}