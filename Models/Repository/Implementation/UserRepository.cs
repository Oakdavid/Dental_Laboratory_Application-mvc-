using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DentalLabDbContext _dbContext;

        public UserRepository(DentalLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            return user;
        }

        public bool Exist(Func<User, bool> predicate)
        {
            return _dbContext.Users.Any(predicate);
        }

        public User Get(Expression<Func<User, bool>> predicate)
        {
            var user = _dbContext.Set<User>()
                .Include(u => u.Role)
                .Include(u => u.Profile)
                .SingleOrDefault(predicate);
            return user;
        }

        public ICollection<User> GetAll(Expression<Func<User, bool>> predicate)
        {
            var user = _dbContext.Set<User>()
                .Include(u => u.Role)
                .Include(u => u.Profile)
                .Where(predicate).ToList();
            return user;
        }

        public IReadOnlyList<User> GetUsers()
        {
            var user = _dbContext.Set<User>()
                .Include(u => u.Role).ToList();
            return user;
        }
    }
}
