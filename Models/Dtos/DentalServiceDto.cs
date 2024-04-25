namespace Dental_lab_Application_MVC_.Models.Dtos
{
    public class DentalServiceDto : BaseResponse
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Code { get; set; } = default!;
        public decimal Cost { get; set; } = default!;
    }

    public class DentalServiceCreateRequestModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Code { get; set; } = default!;
        public decimal Cost { get; set; } = default!;
    }

    public class DentalServiceUpdateRequestModel
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Cost { get; set; } = default!;
        public string Code { get; set; } = default!;
    }
}
