﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SayyehBanTools.ConnectionDB;
using System.Text;

var factory = new ConnectionFactory();
Uri connectionUri = RabbitMQConnection.DefaultConnection();
factory.Uri = new Uri(connectionUri.OriginalString);
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
string routingKey = "order.cancel";
string Exchange = "OrderHeader";
string QueueName = "EmailService";

channel.QueueDeclare(QueueName, false, false, false);
channel.QueueBind(QueueName, Exchange, routingKey);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Received Message " + message);
};
channel.BasicConsume(QueueName, true, consumer);
Console.ReadLine();