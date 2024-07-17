using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCarePro.BLL.Dtos
{
	public class VisitNotificationDto
	{
        public DateOnly Date { get; set; }
        public DailyVisitToReturnDto Visit { get; set; }
    }
}
