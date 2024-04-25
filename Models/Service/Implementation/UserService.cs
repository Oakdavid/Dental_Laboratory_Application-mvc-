using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using System.Linq.Expressions;

namespace Dental_lab_Application_MVC_.Models.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserByEmail(string email)
        {
            var userEmail = _userRepository.Get(u => u.UserEmail == email);
            if(userEmail != null)
            {
                return new UserDto
                {
                    UserEmail = userEmail.UserEmail,
                    UserName = userEmail.UserName,
                    Id = userEmail.Id
                };
            }
            return null;

        }
        public UserDto LoginByEmailOrByUserNameAndPassword(LoginRequestModel loginRequest)
        {
            try
            {
                var login = _userRepository.Get( u => (u.UserName.ToLower() == loginRequest.UserName.ToLower() && u.Password == loginRequest.Password) || (u.UserEmail.ToLower() == loginRequest.UserName.ToLower() && u.Password == loginRequest.Password));
                if (login != null)
                {
                    if (login.Password != loginRequest.Password)
                    {
                        return new UserDto
                        {
                            Message = "invalid credentials",
                            Status = false
                        };
                    }

                    return new UserDto
                    {
                        Id = login.Id,
                        UserName = login.UserName,
                        UserEmail = login.UserEmail,
                        RoleName = login.Role.Name,
                        Message = "successful",
                        Status = true
                    };
                }

                else
                {
                    return new UserDto
                    {
                        Message = "Invalid Credentials",
                        Status = false
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while attempting to log in: {ex.Message}");
                throw;
            }
            
        }
    }
}
