using Dental_lab_Application_MVC_.Models.Enum;

namespace Dental_lab_Application_MVC_.Models.Entites
{
    public class Profile : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; } = default!;
        public string? Bio { get; set; }
        public string? Contact { get; set; }
        public Gender Gender { get; set; } = default!;
        public string? ProfilePicture { get; set; }
        public string? SocialMediaLink { get; set; }
        public string? Interests { get; set; }
        public string? Skills { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
