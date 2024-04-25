using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DentalLabDbContext _dbContext;

        public PatientRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Patient AddPatient(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            return patient;
        }

        public bool Exist(Func<Patient, bool> predicate)
        {
            return _dbContext.Patients.Any(predicate);
        }

        public Patient Get(Expression<Func<Patient, bool>> predicate)
        {
            var patient = _dbContext.Set<Patient>()
                .Include(p => p.Appointments)
                .Include(p => p.User)
                .ThenInclude(p => p.Profile)
                .FirstOrDefault(predicate);
            return patient;
        }


        public ICollection<Patient> GetAll()
        {
            var patient = _dbContext.Set<Patient>()
               .Include(p => p.Appointments) 
               .Include(p => p.User)
               .ThenInclude(p => p.Profile).ToList();
            return patient;
        }

        public ICollection< Patient> GetAllAssignedPatient(Expression<Func<Patient, bool>> predicate)
        {
            var patient = _dbContext.Set<Patient>()
               .Include(p => p.Appointments)
               .Include(p => p.User)
               .ThenInclude(p => p.Profile)
               .Where(predicate).ToList();
            return patient;
        }

        public bool UpdatePatientStatus(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            return true;
        }
    }
}
