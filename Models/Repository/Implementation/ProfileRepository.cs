using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DentalLabDbContext _dbContext;

        public ProfileRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Profile CreateProfile(Profile profile)
        {
            _dbContext.Profiles.Add(profile);
            return profile;
        }

        public Profile Get(Expression<Func<Profile, bool>> predicate)
        {
            var profile = _dbContext.Set<Profile>()
                .Include(p => p.User)
                .SingleOrDefault(predicate);
            return profile;

        }

        public ICollection<Profile> GetAll()
        {
            var profile = _dbContext.Set<Profile>()
                 .Include(p => p.User).ToList();
            return profile;
        }
    }
}
