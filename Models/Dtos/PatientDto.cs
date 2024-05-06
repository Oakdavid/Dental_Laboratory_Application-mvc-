using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Dental_lab_Application_MVC_.Models.Dtos
{
    public class PatientDto : BaseResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } = default!;
        public string CardNo { get; set; } = default!;
        public string? MedicalHistory { get; set; }
        public ICollection<Appointment> Appointments = new HashSet<Appointment>();

        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; } = default!;
        public string? Contact { get; set; }
        public Gender? Gender { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public string? SocialMediaLink { get; set; }
        public string? Interests { get; set; }
        public string? Skills { get; set; }
    }

    public class PatientCreateRequestModel
    {
        public string CardNo { get; set; } = default!;
        public string? MedicalHistory { get; set; }

        public ICollection<Appointment> Appointments = new HashSet<Appointment>();

        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "it is required")]
        [EmailAddress(ErrorMessage = "invalid email address")]
        public string UserEmail { get; set; } = default!;

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
        public string? Contact { get; set; }
        public Gender Gender { get; set; }
    }
    public class UpdatePatientRequestModel
    {
        public string? MedicalHistory { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Address { get; set; }
        public string? Bio { get; set; }
        public string? Contact { get; set; }
        public string? ProfilePicture { get; set; }
        public string? SocialMediaLink { get; set; }
        public string? Interests { get; set; }
        public string? Skills { get; set; }
    }
}
