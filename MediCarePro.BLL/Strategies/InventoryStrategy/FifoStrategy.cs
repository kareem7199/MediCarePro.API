using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.Strategies.InventoryStrategy
{
	public class FifoStrategy : IInventoryStrategy
	{
		public decimal ProcessTransactionAsync(List<Transaction> inventory)
		{
			Queue<Transaction> queue = new Queue<Transaction>();

			foreach (Transaction transaction in inventory)
			{
				if (transaction.Amount >= 0)
				{
					queue.Enqueue(transaction);
				}
				else
				{
					var front = queue.Peek();

					while (front.Quantity <= transaction.Quantity)
					{
						transaction.Quantity -= front.Quantity;
						queue.Dequeue();
						front = queue.Peek();
					}

					if(transaction.Quantity > 0) front.Quantity -= transaction.Quantity;
				}
			}

			decimal inventoryValue = 0;
			foreach (Transaction transaction in queue)
			{
				inventoryValue += (transaction.Quantity * transaction.Amount);
			}

			return inventoryValue;
		}
	}
}
