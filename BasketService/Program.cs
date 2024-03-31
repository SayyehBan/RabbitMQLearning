using RabbitMQ.Client;
using SayyehBanTools.ConnectionDB;
using System.Text;

var factory = new ConnectionFactory();

Uri connectionUri = RabbitMQConnection.DefaultConnection();
factory.Uri = new Uri(connectionUri.OriginalString);
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string routingKey = "order.cancel";
string QueueName = "";

string Exchange = "OrderHeader";
channel.ExchangeDeclare(Exchange, ExchangeType.Headers, false);

string message = $"Send Shopping cart Information to place an order timr : {DateTime.Now.Ticks}";
var body = Encoding.UTF8.GetBytes(message);
var headers = new Dictionary<string, object>
{
    { "subject","order"},
    {"action","create" }
};
var properties = channel.CreateBasicProperties();
properties.Headers = headers;
channel.BasicPublish(exchange: Exchange, routingKey: "", basicProperties: properties, body);
Console.ReadLine();