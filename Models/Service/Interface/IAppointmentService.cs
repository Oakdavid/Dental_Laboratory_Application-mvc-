using Dental_lab_Application_MVC_.Models.Dtos;

namespace Dental_lab_Application_MVC_.Models.Service.Interface
{
    public interface IAppointmentService
    {
        AppointmentDto Add(AppointmentRequestModel obj);
        AppointmentDto GetAppointmentByPatientId(Guid patientId);
        AppointmentDto Get(Guid id);

        List<AppointmentDto> GetAll();
        ICollection<AppointmentDto> GetAllInitialized();
        AppointmentDto GetAppointmentByDoctorId(Guid id);
        bool AssignDoctorToAppointment(string cardNo, Guid doctorId);
        ICollection<AppointmentDto> GetAllAssigned();
    }
}
