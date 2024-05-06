using Dental_lab_Application_MVC_.Models.Dtos;

namespace Dental_lab_Application_MVC_.Models.Service.Interface
{
    public interface IAppointmentService
    {
        AppointmentDto Create(AppointmentRequestModel obj);
        AppointmentDto Get(Guid id);

        List<AppointmentDto> GetAll();
        ICollection<AppointmentDto> GetAllInitializedAppointment();
        bool AssignDoctorToAppointment(Guid patientNo, Guid doctorId);
        ICollection<AppointmentDto> GetAllAssigned();
        AppointmentDto PatientViewBookedAppointment(Guid patientId);
        ICollection<AppointmentDto> GetDoctorAssignedAppointments(Guid doctorId);
        bool CloseAssignedAppointment(Guid doctorId);
        ICollection<AppointmentDto> GetAllClosedAppointment();
    }
}
