using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Enum;

namespace Dental_lab_Application_MVC_.Models.Dtos
{
    public class ProfileDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
        public string? Bio { get; set; }
        public string? Contact { get; set; }
        public Gender Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public string? SocialMediaLink { get; set; }
        public string? Interests { get; set; }
        public string? Skills { get; set; }
        public Guid UserId { get; set; }
    }

    public class ProfileCreateRequestModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!; 
        public string? Bio { get; set; }
        public string? Contact { get; set; }
        public Gender Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public string? SocialMediaLink { get; set; }
        public string? Interests { get; set; }
        public string? Skills { get; set; }
    }
}
