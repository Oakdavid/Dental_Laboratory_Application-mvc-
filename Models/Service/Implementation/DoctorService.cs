using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Dental_lab_Application_MVC_.Models.Service.Interface;

namespace Dental_lab_Application_MVC_.Models.Service.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IDoctorRepository doctorRepository, IUserRepository userRepository, IRoleRepository roleRepository, IProfileRepository profileRepository, IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _profileRepository = profileRepository;
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public DoctorDto Add(CreateDoctorRequestModel requestModel)
        {
            //  bool doctorExist = _doctorRepository.Exist(e => e.LicenseNumber == requestModel.LicenseNumber);
           // bool doctorExist = _doctorRepository.Exist(e => e.User.UserEmail == requestModel.UserEmail);
            var doctorExist = _userRepository.Exist(e => e.UserEmail  == requestModel.User.UserEmail || e.UserName == requestModel.User.UserName);

            if (doctorExist)
            {
                return new DoctorDto
                {
                    Message = "Doctor already exist",
                    Status = false,
                    Data = null,
                };
            }

            var role = _roleRepository.Get(r => r.Name == "Doctor");
            if (role == null)
            {
                return null;
            }
            var user = new User
            {
                UserName = requestModel.User.UserName,
                UserEmail = requestModel.User.UserEmail,
                Password = requestModel.User.Password,
                Role = role,
                RoleId = role.Id,
            };
            _userRepository.AddUser(user);

            Profile profile = new Profile
            {
                FirstName = requestModel.Profile.FirstName,
                LastName = requestModel.Profile.LastName,
                Address = requestModel.Profile.Address,
                DateOfBirth = requestModel.Profile.DateOfBirth,
                Bio = requestModel.Profile.Bio,
                Contact = requestModel.Profile.Contact,
                Gender = requestModel.Profile.Gender,
                ProfilePicture = requestModel.Profile.ProfilePicture,
                SocialMediaLink = requestModel.Profile.SocialMediaLink,
                Interests = requestModel.Profile.Interests,
                Skills = requestModel.Profile.Skills,
                User = user,
                UserId = user.Id,
            };
            _profileRepository.AddProfile(profile);

            var doctor = new Doctor
            {
                Id = user.Id,
                LicenseNumber = requestModel.LicenseNumber,
                Education = requestModel.Education,
                YearsOfExperience = requestModel.YearsOfExperience,
                Specializations = requestModel.Specializations,
                IsAvailable = true,
                UserId = user.Id,
                
            };
            _doctorRepository.AddDoctor(doctor);
            _unitOfWork.Save();

            return new DoctorDto
            {
                LicenseNumber = doctor.LicenseNumber,
                Education = doctor.Education,
                YearsOfExperience = doctor.YearsOfExperience,
                Specializations = doctor.Specializations,
                IsAvailable = true,
                LastName = requestModel.Profile.LastName, // testing
                UserId = user.Id,
                Id = user.Id,
                Message = "Success",
                Status = true,
            };
        }

        public DoctorDto Get(Guid id)
        {
            var doctor = _doctorRepository.Get( d => d.Id == id && !d.IsDeleted);
            if(doctor != null)
            {
                return new DoctorDto
                {
                    LicenseNumber = doctor.LicenseNumber,
                    Education = doctor.Education,
                    YearsOfExperience = doctor.YearsOfExperience,
                    Specializations = doctor.Specializations,
                    IsAvailable = true,
                    UserId = id,
                    DoctorId = id,
                    Id = id,
                    Message = "Successfully updated",
                    Status = true,
                     
                };
            }
            return null;
        }

        public ICollection<DoctorDto> GetAll()
        {
            var doctor = _doctorRepository.GetAll();
            var allDoctors = doctor.Select(d => new DoctorDto
            {
                LicenseNumber = d.LicenseNumber,
                Education = d.Education,
                YearsOfExperience = d.YearsOfExperience,
                Specializations = d.Specializations,
                IsAvailable = true,
                DoctorId = d.Id,
                Id = d.Id,
                UserId = d.Id,

            }).ToList();
            return allDoctors;
        }

        public ICollection<DoctorDto> GetAllAvailableDoctors()
        {
            var doctors = _doctorRepository.GetAll();
            var availableDoctors = doctors.Where(d => d.IsAvailable);
            if(availableDoctors != null)
            {
                var doctorDto = availableDoctors.Select(d => new DoctorDto
                {
                    LicenseNumber = d.LicenseNumber,
                    Education = d.Education,
                    YearsOfExperience = d.YearsOfExperience,
                    Specializations = d.Specializations,
                    IsAvailable = true,
                    DoctorId = d.Id,
                    Id = d.Id
                }).ToList();
                return doctorDto;
            }
            return null;
        }


        public DoctorDto Update(UpdateDoctorRequestModel updateRequestModel, Guid id)
        {
            var existingDoctor = _doctorRepository.Get(u => u.User.Id == id);
            if (existingDoctor != null)
            {
                existingDoctor.User.Profile.FirstName = updateRequestModel.FirstName;
                existingDoctor.User.Profile.LastName = updateRequestModel.LastName;
                existingDoctor.User.Profile.Contact = updateRequestModel.Contact;
                existingDoctor.User.Profile.ProfilePicture = updateRequestModel.ProfilePicture;
                existingDoctor.LicenseNumber = updateRequestModel.LicenseNumber;
                existingDoctor.Education = updateRequestModel.Education;
                existingDoctor.Specializations = updateRequestModel.Specializations;
                existingDoctor.YearsOfExperience = updateRequestModel.YearsOfExperience;
                _doctorRepository.Update(existingDoctor);
                _unitOfWork.Save();

                return new DoctorDto
                {
                    Id = id,
                    FirstName = updateRequestModel.FirstName,
                    LastName = updateRequestModel.LastName,
                    Contact = updateRequestModel.Contact,
                    ProfilePicture = updateRequestModel.ProfilePicture,
                    UserId = existingDoctor.User.Id,
                    Education = updateRequestModel.Education,
                    Specializations = updateRequestModel.Specializations,
                    YearsOfExperience = updateRequestModel.YearsOfExperience,
                    Message = "Profile updated successfully",
                    Status = true,
                };
            }
            else
            {
                return new DoctorDto
                {
                    Message = "Failed to update",
                    Status = false
                };
            }
        }
    }
}
