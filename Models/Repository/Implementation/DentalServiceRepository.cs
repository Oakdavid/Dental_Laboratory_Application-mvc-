using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class DentalServiceRepository : IDentalServiceRepository
    {
        private readonly DentalLabDbContext _dbContext;

        public DentalServiceRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DentalService AddDentalService(DentalService dentalService)
        {
            _dbContext.DentalServices.Add(dentalService);
            return dentalService;
        }

        public bool Exist(Func<DentalService, bool> predicate)
        {
            return _dbContext.DentalServices.Any(predicate);
        }

        public DentalService Get(Guid id)
        {
            var dentalService = _dbContext.DentalServices.SingleOrDefault(d => d.Id == id);
                return dentalService;
        }

        public DentalService Get(Expression<Func<DentalService, bool>> predicate)
        {
            var dentalService = _dbContext.DentalServices.SingleOrDefault(predicate);
            return dentalService;
        }

        public ICollection<DentalService> GetAll()
        {
            var dentalServices = _dbContext.Set<DentalService>().ToList();
            return dentalServices;
        }

        public bool Update(DentalService dentalService)
        {
            _dbContext.DentalServices.Update(dentalService);
            return true;
        }
    }
}
