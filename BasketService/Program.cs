using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

string QueueName = "order.create";

string Exchange = "Order";
channel.ExchangeDeclare(Exchange, ExchangeType.Fanout, false);

string message = $"Send Shopping cart Information to place an order timr : {DateTime.Now.Ticks}";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchange: Exchange, routingKey: "", basicProperties: null, body);
Console.ReadLine();