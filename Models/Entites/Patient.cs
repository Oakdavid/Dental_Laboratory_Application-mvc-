namespace Dental_lab_Application_MVC_.Models.Entites
{
    public class Patient : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public string CardNo { get; set; } = default!;
        public string? MedicalHistory { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>(); 
    }
}
