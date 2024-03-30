using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string QueueName = "zljn7vcbl3t28usw";
channel.QueueDeclare(QueueName, false, false, false, null);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, args) =>
{
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Received Message " + message);
    Thread.Sleep(2000);
};
channel.BasicConsume(QueueName, true, consumer);

Console.ReadLine();