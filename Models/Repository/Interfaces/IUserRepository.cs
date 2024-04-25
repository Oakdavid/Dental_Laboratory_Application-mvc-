using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Interfaces
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User Get(Expression<Func<User, bool>> predicate);
        IReadOnlyList<User> GetUsers();
        ICollection<User> GetAll(Expression<Func<User, bool>> predicate);
        bool Exist(Func<User, bool> predicate);
    }
}
