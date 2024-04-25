using Dental_lab_Application_MVC_.Models.Context;
using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Implementation;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dental_lab_Application_MVC_.Models.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserRepository _userRepository;

        public PatientService(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IRoleRepository roleRepository, IProfileRepository profileRepository, IAppointmentRepository appointmentRepository, IUserRepository userRepository, IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _profileRepository = profileRepository;
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
        }

        public PatientDto Add(PatientCreateRequestModel requestModel)
        {
           var exist = _userRepository.Exist( p => p.UserEmail == requestModel.UserEmail || p.UserName == requestModel.UserName);
            //var exist = _userRepository.Exist(x => x.UserName == mentor.UserName || x.Email == mentor.Email);
            if ( exist )
            {
                return new PatientDto
                {
                    Message = "Patient already Exist",
                    Status = false,
                    Data = null,
                };
            }
            var role = _roleRepository.Get(r => r.Name == "Patient");
            if (role == null)
            {
                return null;
            }
            var user = new User
            {
                UserName = requestModel.UserName,
                UserEmail = requestModel.UserEmail,
                Password = requestModel.Password,
                Role = role,
                RoleId = role.Id,
            };
            _userRepository.AddUser(user);

            Profile profile = new Profile
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Address = requestModel.Address,
                DateOfBirth = requestModel.DateOfBirth,
                Contact = requestModel.Contact,
                Gender = requestModel.Gender,
                //Bio = requestModel.Bio,
                //ProfilePicture =requestModel.ProfilePicture,
                //SocialMediaLink = requestModel.SocialMediaLink,
                //Interests = requestModel.Interests,
                //Skills = requestModel.Skills,
                User = user,
                UserId = user.Id,
            };
            _profileRepository.AddProfile(profile);
            Patient patient = new Patient
            {
                CardNo = $"RYC/CARDNO/{new Random().Next(01, 100)}",
                Id = user.Id,
                MedicalHistory = requestModel.MedicalHistory,
                Appointments = requestModel.Appointments,
                UserId = user.Id,
            };
            _patientRepository.AddPatient(patient);
            _unitOfWork.Save();

            return new PatientDto
            {
                UserId = user.Id,
                CardNo = patient.CardNo,
                MedicalHistory = patient.MedicalHistory,
                Appointments = patient.Appointments,
                Contact = profile.Contact,
                Gender = profile.Gender,
                Address = profile.Address,
                DateOfBirth = profile.DateOfBirth,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Status = true,
                Message = "Successfully Created",
            };
        }
        public PatientDto Get(Guid id)
        {
            var getPatient = _patientRepository.Get(p => p.Id == id);
            if (getPatient != null && !getPatient.IsDeleted)
            {
                var profile = _profileRepository.Get(p => p.Id == id);
                var getAppointment = _appointmentRepository.GetAppointment(getPatient.Id);
                return new PatientDto
                {
                    UserId = getPatient.Id,
                    CardNo = getPatient.CardNo,
                    MedicalHistory = getPatient.MedicalHistory,
                    Appointments = getPatient.Appointments,
                    Contact = profile?.Contact,
                    Gender = profile?.Gender,
                    Address = profile?.Address,
                    DateOfBirth = profile?.DateOfBirth,
                    FirstName = profile?.FirstName,
                    LastName = profile?.LastName,
                };
            }
            return null;
        }

        public ICollection<PatientDto> GetAll()
        {
          //  var appointments = _appointmentRepository.Get(a => a.AppointmentStatus == Enum.AppointmentStatus.Initialized);
            var allPatients = _patientRepository.GetAll();
            var patientProfile = _profileRepository.GetAll();
            var patients = allPatients.Select(p => new PatientDto
            {
                UserId = p.Id,
                CardNo = p.CardNo,
                MedicalHistory = p.MedicalHistory,
                Appointments = p.Appointments,
                Id = p.Id,
                FirstName = patientProfile.FirstOrDefault(p => p.UserId == p.Id)?.FirstName,
                LastName = patientProfile.FirstOrDefault(p => p.UserId == p.Id)?.LastName,
                Gender = patientProfile.FirstOrDefault(p => p.UserId == p.Id)?.Gender,

            }).ToList();
            return patients;
        }

        public ICollection<PatientDto> GetAllPatientAssigned(Guid doctorId)     // check it
        {
            var allAssignedPatient = _patientRepository.GetAllAssignedPatient( p => p.User.Id == doctorId ); // cross checking if it valid
            if(allAssignedPatient != null && allAssignedPatient.Any())
            {
                var assignedPatients = allAssignedPatient.Select(allAssignedPatient => new PatientDto
                {
                    UserId = allAssignedPatient.Id,
                    CardNo = allAssignedPatient.CardNo,
                    MedicalHistory = allAssignedPatient.MedicalHistory,
                    Appointments = allAssignedPatient.Appointments,
                    Message = "Successfully Assigned",
                    Status = true,
                }).ToList();
                return assignedPatients;
            }
            return null;
            //return new List<PatientDto>();
        }

        public PatientDto Update(UpdatePatientRequestModel updateRequestModel, Guid id)
        {
            var existingPatient = _patientRepository.Get(p => p.User.Id == id);
            if(existingPatient != null)
            {
                existingPatient.User.Profile.FirstName = updateRequestModel.FirstName;
                existingPatient.User.Profile.LastName = updateRequestModel.LastName;
                existingPatient.User.Profile.Address = updateRequestModel.Address;
                existingPatient.User.Profile.Bio = updateRequestModel.Bio;
                existingPatient.User.Profile.Contact = updateRequestModel.Contact;
                existingPatient.User.Profile.ProfilePicture = updateRequestModel.ProfilePicture;
                existingPatient.User.Profile.SocialMediaLink = updateRequestModel.SocialMediaLink;
                existingPatient.User.Profile.Interests = updateRequestModel.Interests;
                existingPatient.User.Profile.Skills = updateRequestModel.Skills;
                _patientRepository.UpdatePatientStatus(existingPatient);
                _unitOfWork.Save();

                return new PatientDto
                {
                    Id = id,
                    FirstName = updateRequestModel.FirstName,
                    LastName = updateRequestModel.LastName,
                    Address = updateRequestModel.Address,
                    Bio = updateRequestModel.Bio,
                    Contact = updateRequestModel.Contact,
                    ProfilePicture = updateRequestModel.ProfilePicture,
                    SocialMediaLink = updateRequestModel.SocialMediaLink,
                    Interests = updateRequestModel.Interests,
                    Skills = updateRequestModel.Skills,
                    MedicalHistory = updateRequestModel.MedicalHistory,
                    Message = "Profile Updated successfully",
                    Status = true,
                };
            }
            else
            {
                return new PatientDto
                {
                    Message = "Failed to update Profile",
                    Status = false
                };

            }
        }
    }
}
