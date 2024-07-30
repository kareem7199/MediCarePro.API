using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.DAL.Specifications
{
	public class TransactionSpec : BaseSpecifications<Transaction>
	{
		public TransactionSpec(int itemId)
			: base(T => T.ItemId == itemId)
		{

		}
	}
}
