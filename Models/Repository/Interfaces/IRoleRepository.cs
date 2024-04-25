using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Role AddRole(Role role);
        Role Get(Expression<Func<Role, bool>> predicate);
        IReadOnlyList<Role> GetAllRoles();
        ICollection<Role> GetAll(Expression<Func<Role, bool>> predicate);
    }
}
