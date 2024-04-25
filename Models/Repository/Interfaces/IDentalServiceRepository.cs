using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Interfaces
{
    public interface IDentalServiceRepository
    {
        DentalService AddDentalService(DentalService dentalService);
        DentalService Get(Guid id);
        bool Exist(Func<DentalService, bool> predicate);
        DentalService Get(Expression<Func<DentalService, bool>> predicate);
        ICollection<DentalService> GetAll();
        bool Update(DentalService dentalService);



    }
}
