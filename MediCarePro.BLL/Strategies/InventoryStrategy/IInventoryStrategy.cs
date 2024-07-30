using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.Strategies.InventoryStrategy
{
	public interface IInventoryStrategy
	{
		decimal ProcessTransactionAsync(List<Transaction> inventory);
	}
}
