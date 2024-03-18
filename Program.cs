using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var con = factory.CreateConnection())

using (var channel = con.CreateModel())
{
    channel.QueueDeclare(queue: "saudacao01", durable: false, exclusive: false, autoDelete: false, arguments: null);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) => {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine("Msg recebida: " + message);
    };

    channel.BasicConsume(queue: "saudacao01", autoAck: true, consumer: consumer);

    Console.ReadLine();
}


