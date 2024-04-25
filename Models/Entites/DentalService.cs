namespace Dental_lab_Application_MVC_.Models.Entites
{
    public class DentalService : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Code { get; set; } = default!;
        public decimal Cost { get; set; } = default!;
    }
}