using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediCarePro.DAL.Data.Entities;

namespace MediCarePro.BLL.Services.ReceptionScreenService
{
    public interface IReceptionScreenService
    {
        public Task<IReadOnlyList<Specialty>> GetSpecialtiesAsync();
        public Task<IReadOnlyList<Patient>> GetPatientsAsync(string? serachTerm);
        public Task<IReadOnlyList<PhysicianSchedule>> GetPhysicianScheduleAsync(string id);
        public Task<Patient?> GetPatientAsync(int id);
        public Task<IReadOnlyList<PhysicianSchedule>> GetFilteredPhysicianScheduleAsync(string physicianId, TimeOnly startTime, Day day);
    }
}
