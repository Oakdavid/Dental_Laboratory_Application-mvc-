using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DentalLabDbContext _dbContext;

        public DoctorRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Doctor AddDoctor(Doctor doctor)
        {
           _dbContext.Doctors.Add(doctor);
            return doctor;
        }

        public bool Exist(Func<Doctor, bool> predicate)
        {
            return _dbContext.Doctors.Any(predicate);
        }

        public Doctor Get(Func<Doctor, bool> predicate)
        {
            var doctor = _dbContext.Set<Doctor>()
                .Include(d => d.Appointments) 
                .Include(d => d.User)
                .ThenInclude(d => d.Profile)
                .SingleOrDefault(predicate);
            return doctor;
        }

        public ICollection<Doctor> GetAll()
        {
            var doctor = _dbContext.Set<Doctor>()
                .Include(d => d.Appointments)
                .Include(d => d.User)
                .ThenInclude(d => d.Profile).ToList();
            return doctor;
        }

        public bool Update(Doctor doctor)
        {
            _dbContext.Doctors.Update(doctor);
            return true;
        }

        public bool UpdateDoctorStatus(Doctor doctor)
        {
            _dbContext.Doctors.Update(doctor);
            return true;
        }
    }
}
