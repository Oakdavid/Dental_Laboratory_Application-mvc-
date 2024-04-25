using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Repository.Interfaces
{
    public interface IProfileRepository
    {
        Profile AddProfile(Profile profile);
        Profile Get(Expression<Func<Profile, bool>> predicate);
        ICollection<Profile> GetAll();
    }
}
