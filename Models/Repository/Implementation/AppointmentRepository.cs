using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DentalLabDbContext _dbContext;

        public AppointmentRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Appointment CreateAppointment(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
            return appointment;
        }

        public bool Exist(Func<Appointment, bool> predicate)
        {
            return _dbContext.Appointments.Any(predicate);
        }

        public Appointment Get(Expression<Func<Appointment, bool>> predicate)
        {
            var appointments = _dbContext.Set<Appointment>()
                 .Include(a => a.Patient)
                 .Include(a => a.Doctor)
                 .SingleOrDefault(predicate);
            return appointments;
        }

        public ICollection<Appointment> GetAll(Expression<Func<Appointment, bool>> predicate)
        {
            var appointments = _dbContext.Set<Appointment>()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(predicate).ToList();
            return appointments;
        }

        public ICollection<Appointment> GetAll()
        {
            var appointments = _dbContext.Set<Appointment>()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.IsDeleted == false) .ToList();
            return appointments;
        }

        public Appointment GetAppointment(Guid PatientId)
        {
            var appointments = _dbContext.Set<Appointment>()
               .Include(a => a.Patient)
               .Include(a => a.Doctor)
               .SingleOrDefault(a => a.PatientId == PatientId);
            return appointments;
        }

        public bool Update(Func<Appointment, bool> predicate, Appointment newValues)
        {
            var appointmentToUpdate = _dbContext.Appointments.Where(predicate);
            foreach (var appointment in appointmentToUpdate)
            {
                appointment.AppointmentStatus = newValues.AppointmentStatus;
                appointment.DateOfAppointment = newValues.DateOfAppointment;
            }
            return appointmentToUpdate.Any();

        }

        public bool Update(Appointment appointment)
        {
           _dbContext.Appointments.Update(appointment);
            return true;
        }

        public bool UpdateAppointmentWithDoctorId(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            return true;
        }
    }
}
