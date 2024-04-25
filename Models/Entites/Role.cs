namespace Dental_lab_Application_MVC_.Models.Entites
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
