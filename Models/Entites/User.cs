namespace Dental_lab_Application_MVC_.Models.Entites
{
    public class User : BaseEntity
    {

        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? HashSalt { get; set; } 
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
        public Profile? Profile { get; set; }
    }
}
