using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MediCarePro.BLL.Services.RabbitMqService
{
	public class RabbitMqService : IRabbitMqService
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;

		public RabbitMqService()
		{
			
			var factory = new ConnectionFactory()
			{
				HostName = "localhost",
				Port = 5672
			};

			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();

			_channel.QueueDeclare(queue: "task_queue",
								 durable: true,
								 exclusive: false,
								 autoDelete: false,
								 arguments: null);

			StartConsuming();
		}

		public void SendMessage(string message)
		{
			var body = Encoding.UTF8.GetBytes(message);

			_channel.BasicPublish(exchange: "",
								  routingKey: "task_queue",
								  basicProperties: null,
								  body: body);

			Console.WriteLine($"Sent message: {message}");
		}

		public void StartConsuming()
		{
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine($"Received message: {message}");
			};

			_channel.BasicConsume(queue: "task_queue",
								 autoAck: true,
								 consumer: consumer);
		}

		public void Dispose()
		{
			_channel?.Close();
			_connection?.Close();
		}
	}
}
