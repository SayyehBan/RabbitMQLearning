using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SayyehBanTools.ConnectionDB;
using System.Text;

var factory = new ConnectionFactory();
Uri connectionUri = RabbitMQConnection.DefaultConnection();
factory.Uri = new Uri(connectionUri.OriginalString);
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string routingKey = "order.*";


string Exchange = "OrderHeader";
string QueueName = "orderService";
var headers = new Dictionary<string, object>
{
    { "subject","order"},
    {"action","create" },
    {"x-match","any" }
};

channel.QueueDeclare(QueueName, false, true, false);
channel.QueueBind(QueueName, Exchange, "", headers);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    var subject = Encoding.UTF8.GetString(args.BasicProperties.Headers["subject"] as byte[]);
    Console.WriteLine($" subjext :{subject} Received Message " + message);
};
channel.BasicConsume(QueueName, true, consumer);

Console.ReadLine();