using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.DAL.Specifications
{
	public class VisitSpec : BaseSpecifications<Visit>
	{
        public VisitSpec(string physicianId, DateTime from, DateTime to)
            :base(V => V.AccountId == physicianId && V.Date >= from && V.Date <= to)
        {
            Includes.Add(V => V.Patient);
            OrderBy = V => V.Date;
        }
    }
}
