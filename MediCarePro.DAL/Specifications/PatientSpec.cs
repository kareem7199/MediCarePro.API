using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.DAL.Specifications
{
	public class PatientSpec : BaseSpecifications<Patient>
	{
		public PatientSpec(string? searchTerm)
			: base(p =>
			(string.IsNullOrEmpty(searchTerm) ||
			p.Name.ToLower().Contains(searchTerm.ToLower()) ||
			p.PhoneNumber.Contains(searchTerm) ||
			p.Id.ToString().Contains(searchTerm)))
		{

		}
	}
}
