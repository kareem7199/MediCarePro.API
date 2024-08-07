﻿using System;
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
            Includes.Add(V => V.Diagnoses);
            OrderBy = V => V.Date;
        }

        public VisitSpec(int id)
            :base(V => V.Id == id)
        {
            Includes.Add(V => V.Patient);
            Includes.Add(V => V.Physician);
        }
    }
}
