using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Enum;

namespace Dental_lab_Application_MVC_.Models.Dtos
{
    public class DoctorDto : BaseResponse
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Contact { get; set; }
        public string? ProfilePicture { get; set; }
        public string LicenseNumber { get; set; } = default!;
        public string Education { get; set; } = default!;
        public int YearsOfExperience { get; set; } = default!;
        public string? Specializations { get; set; } 
        public bool IsAvailable { get; set; } = default!;
        public Guid? DoctorId { get; set; }
    }

    public class CreateDoctorRequestModel
    {
        public string LicenseNumber { get; set; } = default!;
        public string Education { get; set; } = default!;
        public int YearsOfExperience { get; set; } = default!;
        public string Specializations { get; set; } = default!;
        public CreateUserRequestModel User { get; set; } = default!;
        public ProfileCreateRequestModel Profile { get; set; } = default!;
    }

    public class UpdateDoctorRequestModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Contact { get; set; }
        public string? ProfilePicture { get; set; }
        public Guid Id { get; set; }
        public string LicenseNumber { get; set; } = default!; // readonly
        public string Education { get; set; } = default!;
        public int YearsOfExperience { get; set; } = default!;
        public string Specializations { get; set; } = default!;
    }
}
