namespace Dental_lab_Application_MVC_.Models.Entites
{
    public class Report : BaseEntity
    {
        public Guid? AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = default!;
        public string? ReportContent { get; set; }
        public string PatientComplain { get; set; } = default!;

    }
}
