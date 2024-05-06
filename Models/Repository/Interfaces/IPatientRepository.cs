using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Interfaces
{
    public interface IPatientRepository
    {
        Patient CreatePatient(Patient patient);
        bool Exist(Func<Patient, bool> predicate);
        bool UpdatePatientStatus(Patient patient);
        Patient Get(Expression<Func<Patient, bool>> predicate);
        ICollection<Patient> GetAllAssignedPatient(Expression<Func<Patient, bool>> predicate);
        ICollection<Patient> GetAll();
    }
}
