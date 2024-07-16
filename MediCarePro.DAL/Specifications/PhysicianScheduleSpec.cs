using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.DAL.Specifications
{
	public class PhysicianScheduleSpec : BaseSpecifications<PhysicianSchedule>
	{
		public PhysicianScheduleSpec(string physicianId)
			: base(PS => PS.AccountId == physicianId)
		{

		}
		public PhysicianScheduleSpec(string physicianId, TimeOnly startTime, Day day)
			: base(
				  PS => PS.StartTime <= startTime &&
				  PS.EndTime >= startTime &&
				  PS.AccountId == physicianId &&
				  PS.Day == day)
		{

		}
	}
}
