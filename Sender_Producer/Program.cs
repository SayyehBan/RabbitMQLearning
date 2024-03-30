using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string QueueName = "zljn7vcbl3t28usw";
channel.QueueDeclare(QueueName, false, false, false, null);
for (int i = 0; i < 30; i++)
{
    string message = $"This is a test meassage from my sender at :{DateTime.Now.Ticks}";
    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish("", QueueName, null, body);
}



channel.Close();
connection.Close();
Console.ReadLine();