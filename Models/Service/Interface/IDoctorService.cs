using Dental_lab_Application_MVC_.Models.Dtos;

namespace Dental_lab_Application_MVC_.Models.Service.Interface
{
    public interface IDoctorService
    {
        DoctorDto Add(CreateDoctorRequestModel requestModel);
        DoctorDto Get(Guid id);
        ICollection<DoctorDto> GetAll();
        ICollection<DoctorDto> GetAllAvailableDoctors();
        DoctorDto Update(UpdateDoctorRequestModel updateRequestModel, Guid id);
    }
}
