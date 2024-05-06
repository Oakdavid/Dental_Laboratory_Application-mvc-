using Dental_lab_Application_MVC_.Models.Entites;
using System.ComponentModel.DataAnnotations;

namespace Dental_lab_Application_MVC_.Models.Dtos
{
    public class UserDto : BaseResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }

    public class CreateUserRequestModel
    {
      
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "it is required")]
        [EmailAddress(ErrorMessage = "invalid email address")]
        public string UserEmail { get; set; } = default!;

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Password { get; set; } = default!;
    }

    public class LoginRequestModel
    {
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
