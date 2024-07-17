using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class DailyVisitToReturnDto
	{
        public int Id { get; set; }
        public decimal PhysicanFees { get; set; }
        public string? Diagnosis { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
