using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly DentalLabDbContext _dbContext;

        public UnitOfWorkRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
