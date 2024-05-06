using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Enum;

namespace Dental_lab_Application_MVC_.Models.Dtos
{
    public class AppointmentDto : BaseResponse
    {
        public Guid Id { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; } = default!;
        public AppointmentType AppointmentType { get; set; } = default!;
        public string BriefMessage { get; set; } = default!;
    }

    public class AppointmentRequestModel
    {
        public Guid UserId { get; set; }
        public Guid? PatientId { get; set; }
        public AppointmentStatus? AppointmentStatus { get; set; }
        public AppointmentType AppointmentType { get; set; } = default!;
        public string BriefMessage { get; set; } = default!;
    }
    public class AssignDoctorToAppointment(Guid patientId, Guid doctorId)
    {

        public Guid DoctorId { get; set; }
        public string cardNo { get; set; }
    }
}
