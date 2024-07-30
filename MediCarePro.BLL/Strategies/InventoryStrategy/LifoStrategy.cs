using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MediCarePro.BLL.Strategies.InventoryStrategy
{
	public class LifoStrategy : IInventoryStrategy
	{
		public decimal ProcessTransactionAsync(List<Transaction> inventory)
		{
			Stack<Transaction> stack = new Stack<Transaction>();

			foreach (Transaction transaction in inventory)
			{
				if (transaction.Amount >= 0)
				{
					stack.Push(transaction);
				}
				else
				{
					var front = stack.Peek();

					while (front.Quantity <= transaction.Quantity)
					{
						transaction.Quantity -= front.Quantity;
						stack.Pop();
						front = stack.Peek();
					}

					if (transaction.Quantity > 0) front.Quantity -= transaction.Quantity;
				}
			}

			decimal inventoryValue = 0;
			foreach (Transaction transaction in stack)
			{
				inventoryValue += (transaction.Quantity * transaction.Amount);
			}

			return inventoryValue;
		}
	}
}
