using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Service.Interface
{
    public interface IDentalServiceService
    {
        DentalServiceDto Add(DentalServiceCreateRequestModel createModel);
        DentalServiceDto GetByCode(string code);
        DentalServiceDto GetByName(string name);
        ICollection<DentalServiceDto> GetAll();
        DentalServiceDto Update(DentalServiceUpdateRequestModel dentalServiceUpdateModel);
        DentalServiceDto Get(Guid id);
    }
}
