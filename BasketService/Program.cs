using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string routingKey = "order.cancel";
string QueueName =  "";

string Exchange = "OrderTopic";
channel.ExchangeDeclare(Exchange, ExchangeType.Topic, false);

string message = $"Send Shopping cart Information to place an order timr : {DateTime.Now.Ticks}";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchange: Exchange, routingKey: routingKey, basicProperties: null, body);
Console.ReadLine();