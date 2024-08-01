using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Services.RabbitMqService
{
	public interface IRabbitMqService : IDisposable
	{
		void SendMessage(string message);
	}
}
