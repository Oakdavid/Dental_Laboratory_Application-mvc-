using Dental_lab_Application_MVC_.Models.Dtos;

namespace Dental_lab_Application_MVC_.Models.Service.Interface
{
    public interface IPatientService
    {
        PatientDto Create(PatientCreateRequestModel requestModel);
        PatientDto Get(Guid id);
        ICollection<PatientDto> GetAll();
        ICollection<PatientDto> GetAllPatientAssigned(Guid doctorId);      // is it valid
        PatientDto Update(UpdatePatientRequestModel updateRequestModel, Guid id);
    }
}
