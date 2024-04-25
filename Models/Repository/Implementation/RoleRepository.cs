using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DentalLabDbContext _dbContext;

        public RoleRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Role AddRole(Role role)
        {
            _dbContext.Roles.Add(role);
            return role;
        }

        public Role Get(Expression<Func<Role, bool>> predicate)
        {
            var role = _dbContext.Set<Role>()
                .Include(r => r.Users)
                .SingleOrDefault(predicate);
            return role;
        }

        public ICollection<Role> GetAll(Expression<Func<Role, bool>> predicate)
        {
            var role = _dbContext.Set<Role>()
              .Include(r => r.Users)
              .Where(predicate).ToList();
            return role;
        }

        public IReadOnlyList<Role> GetAllRoles()
        {
            var role = _dbContext.Set<Role>()
              .Include(r => r.Users).ToList();
            return role;
        }
    }
}
