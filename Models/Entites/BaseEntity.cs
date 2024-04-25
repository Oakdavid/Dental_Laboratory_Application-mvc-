namespace Dental_lab_Application_MVC_.Models.Entites
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = new Guid();

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
