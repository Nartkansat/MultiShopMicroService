﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MultiShop.RabbitMQMessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",

            };

            var connection = connectionFactory.CreateConnection();

            var channel = connection.CreateModel();
            channel.QueueDeclare("Kuyruk2", false, false, false, arguments: null);

            var messageContent = "RabbitMQ Kuyruk2 mesajı";
            var byteMessageContent = Encoding.UTF8.GetBytes(messageContent);

            channel.BasicPublish(exchange: "", routingKey: "Kuyruk2", basicProperties: null, body: byteMessageContent);

            return Ok("Mesaj Kuyruğa alınmıştır.");
        }

        public static string message;

        [HttpGet]
        public IActionResult ReadMessage()
        {
            var factory = new ConnectionFactory();

            factory.HostName = "localhost";

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, x) =>
            {
                var byteMessage = x.Body.ToArray();
                message = Encoding.UTF8.GetString(byteMessage);
            };


            channel.BasicConsume(queue: "Kuyruk1", autoAck: false, consumer: consumer);

            return Ok(message);

        }
    }
}
