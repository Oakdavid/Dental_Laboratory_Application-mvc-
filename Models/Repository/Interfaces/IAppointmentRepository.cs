using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        Appointment AddAppointment(Appointment appointment);
        Appointment GetAppointment(Guid PatientId);
        Appointment Get(Expression<Func<Appointment, bool>> predicate);
        ICollection<Appointment> GetAll(Expression<Func<Appointment, bool>> predicate);
        bool Update(Appointment appointment);
        bool UpdateAppointmentWithDoctorId(Appointment appointment);
        bool Exist(Func<Appointment, bool> predicate);
        ICollection<Appointment> GetAll();
    }
}
