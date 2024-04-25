using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Service.Interface
{
    public interface IUserService
    {
        UserDto LoginByEmailOrByUserNameAndPassword(LoginRequestModel loginRequest);
        ICollection<User> GetAll();
        UserDto GetUserByEmail(string email);
    }
}
