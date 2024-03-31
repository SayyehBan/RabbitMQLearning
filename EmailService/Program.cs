﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string Exchange = "Order";
string QueueName = "order.Cancel";

channel.QueueDeclare(QueueName, false, false, false);
channel.QueueBind(QueueName, Exchange, "");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Received Message " + message);
};
channel.BasicConsume(QueueName, true, consumer);
Console.ReadLine();