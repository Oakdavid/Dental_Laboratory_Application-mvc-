using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using System.Runtime.CompilerServices;

namespace Dental_lab_Application_MVC_.Models.Service.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public AppointmentDto Add(AppointmentRequestModel appointmentRequestModel)
        {
            var patient = _patientRepository.Get(p => p.UserId == appointmentRequestModel.PatientId);
            if(patient == null)
            {
                return new AppointmentDto
                {
                    Message = "Patient appointment not found",
                    Status = false,
                    Data = null,
                };
            }
            var existingAppointment = _appointmentRepository.Get(a => a.PatientId == patient.UserId);
            if (existingAppointment != null)
            {
                return new AppointmentDto
                {
                    Message = "An appointment already exist",
                    Status = false,
                    Data = null,
                };
            }  
            var appointment = new Appointment
            {
                    //Id = patient.Id,
                    //PatientId = patient.UserId,
                    //DoctorId = appointmentRequestModel.DoctorId,
                    DateOfAppointment = DateTime.Now,
                  //  AppointmentStatus = Enum.AppointmentStatus.Initialized,
                    AppointmentType = Enum.AppointmentType.Physical,
                    BriefMessage = appointmentRequestModel.BriefMessage
            };
             _appointmentRepository.AddAppointment(appointment);
             _unitOfWork.Save();
            return new AppointmentDto
            {
                    Id = appointment.Id,
                    PatientId = appointment.PatientId,
                    DoctorId = appointment.DoctorId,
                    DateOfAppointment = appointment.DateOfAppointment,
                    AppointmentStatus = appointment.AppointmentStatus,
                    AppointmentType = appointment.AppointmentType,
                    BriefMessage = appointment.BriefMessage,
            };
            
        }

        public bool AssignDoctorToAppointment(string cardNo, Guid doctorId)
        {
            var appointment = _appointmentRepository.Get( a => a.Patient.CardNo == cardNo  && a.AppointmentStatus == Enum.AppointmentStatus.Initialized);
            var doctor = _doctorRepository.Get(d => d.Id == doctorId);
            if( appointment == null || doctor == null) 
            {
                return false;
            }
            appointment.DoctorId = doctorId;
            appointment.AppointmentStatus = Enum.AppointmentStatus.Assigned;
            doctor.IsAvailable = true;
            _appointmentRepository.Update(appointment);
            _doctorRepository.UpdateDoctorStatus(doctor);
            return true;
        }

        public AppointmentDto Get(Guid id)
        {
           var getAppointment = _appointmentRepository.Get( a => a.Id == id );
            if(getAppointment != null)
            {
                return new AppointmentDto
                {
                    Id = id,
                    DoctorId = getAppointment.DoctorId,
                    PatientId = getAppointment.PatientId,
                    AppointmentStatus = getAppointment.AppointmentStatus,
                    AppointmentType = getAppointment.AppointmentType,
                    DateOfAppointment = getAppointment.DateOfAppointment,
                    BriefMessage = getAppointment.BriefMessage,
                    Message = " Appointment found",
                    Status = true,
                };
            }
            return new AppointmentDto
            {
                Message = "Appointment not found",
                Status = false,
                Data = null,
            };
        }

        public List<AppointmentDto> GetAll()
        {
           var getAllAppointment = _appointmentRepository.GetAll();

           var allAppointment = getAllAppointment.Select(a => new AppointmentDto
           {
                Id = a.Id,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentStatus = a.AppointmentStatus,
                AppointmentType = a.AppointmentType,
                DateOfAppointment = a.DateOfAppointment,
               BriefMessage = a.BriefMessage,
                Message = "Appointment Found",
                Status = true,
           }).ToList();
           
            if(getAllAppointment.Any())
            { 
                return allAppointment;
            }

            return new List<AppointmentDto> 
            {
               new AppointmentDto
               {
                   Status = false,
                   Message = "No Appointment found"
               }
            };
        }

        public ICollection<AppointmentDto> GetAllAssigned()
        {
            var allAssignedAppointment = _appointmentRepository.GetAll();

            var assignedAppointment = allAssignedAppointment.Where( a => a.AppointmentStatus == Enum.AppointmentStatus.Assigned );

            var assignedAppointmentDtos = assignedAppointment.Select(a => new AppointmentDto
            {
               Id = a.Id,
               DoctorId = a.DoctorId,
               PatientId = a.PatientId,
               AppointmentStatus = a.AppointmentStatus,
               AppointmentType = a.AppointmentType,
               DateOfAppointment= a.DateOfAppointment,
                BriefMessage = a.BriefMessage,
               Message = "Found",
               Status= true,
            }).ToList();

            if( assignedAppointmentDtos.Any() )
            {
                return assignedAppointmentDtos;
            }
            
            return new List<AppointmentDto>
            {
                new AppointmentDto
                {
                    Message = "No assigned appointments found",
                    Status = false
                }
            };
        }

        public ICollection<AppointmentDto> GetAllInitialized()
        {
            var initialized = _appointmentRepository.GetAll();

            var allInitialized = initialized.Where( a => a.AppointmentStatus == Enum.AppointmentStatus.Initialized);
            var allInitializedDto = allInitialized.Select(a => new AppointmentDto
            {
                Id = a.Id,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                AppointmentStatus = a.AppointmentStatus,
                AppointmentType = a.AppointmentType,
                DateOfAppointment = a.DateOfAppointment,
                BriefMessage = a.BriefMessage,
                Message = "Found",
                Status = true,
            }).ToList();

            if(allInitializedDto.Any() )
            {
                return allInitializedDto;
            }

            return new List<AppointmentDto>
            {
                new AppointmentDto
                {
                    Message = "No initialized appointments found",
                    Status = false
                }
            };
        }

        public AppointmentDto GetAppointmentByDoctorId(Guid id)
        {
            var getAppointmentByDoctorId = _appointmentRepository.Get( a => a.DoctorId == id );
            if( getAppointmentByDoctorId != null )
            {
                return new AppointmentDto
                {
                    Id = getAppointmentByDoctorId.Id,
                    DoctorId = getAppointmentByDoctorId.DoctorId,
                    PatientId = getAppointmentByDoctorId.PatientId,
                    AppointmentStatus = getAppointmentByDoctorId.AppointmentStatus,
                    AppointmentType = getAppointmentByDoctorId.AppointmentType,
                    DateOfAppointment = getAppointmentByDoctorId.DateOfAppointment,
                    BriefMessage = getAppointmentByDoctorId.BriefMessage,
                    Message = "Found",
                    Status = true,
                };
            }

            return null;
        }

        public AppointmentDto GetAppointmentByPatientId(Guid patientId)
        {
            var getAppointmentByPatientId = _appointmentRepository.Get(a => a.PatientId == patientId);
            if (getAppointmentByPatientId != null)
            {
                return new AppointmentDto
                {
                    Id = getAppointmentByPatientId.Id,
                    DoctorId = getAppointmentByPatientId.DoctorId,
                    PatientId = getAppointmentByPatientId.PatientId,
                    AppointmentStatus = getAppointmentByPatientId.AppointmentStatus,
                    AppointmentType = getAppointmentByPatientId.AppointmentType,
                    DateOfAppointment = getAppointmentByPatientId.DateOfAppointment,
                    BriefMessage = getAppointmentByPatientId.BriefMessage,
                    Message = "Found",
                    Status = true,
                };
            }

            return null;
        }
    }
}
