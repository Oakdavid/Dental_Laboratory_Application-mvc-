namespace Dental_lab_Application_MVC_.Models.Entites
{
    public class Doctor : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public string Education { get; set; } = default!;
        public int YearsOfExperience { get; set; } = default!;
        public string? Specializations { get; set; }
        public bool IsAvailable { get; set; } = default!;
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    }
}
