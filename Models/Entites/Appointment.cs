using Dental_lab_Application_MVC_.Models.Enum;

namespace Dental_lab_Application_MVC_.Models.Entites
{
    public class Appointment : BaseEntity
    {
        public Guid? PatientId { get; set; } 
        public Patient Patient { get; set; } = default!;
        public Guid? DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public DateTime? DateOfAppointment { get; set; }
        //public User Services { get; set; } = default!;  // i dont know how i added this
        public Report Reports { get; set; } = default!;
        public AppointmentStatus AppointmentStatus { get; set; } = default!;
        public AppointmentType AppointmentType { get; set; } = default!;
        public string BriefMessage { get; set; } = default!;
    }
}
