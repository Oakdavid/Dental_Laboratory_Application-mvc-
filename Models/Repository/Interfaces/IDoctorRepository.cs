using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Interfaces
{
    public interface IDoctorRepository
    {
        Doctor AddDoctor(Doctor doctor);
        bool Exist(Func<Doctor, bool> predicate);
        Doctor Get(Func<Doctor, bool> predicate);
        ICollection<Doctor> GetAll();
        bool Update(Doctor doctor);
        bool UpdateDoctorStatus(Doctor doctor);
    }
}
