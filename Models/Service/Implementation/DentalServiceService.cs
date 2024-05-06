using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Repository.Interfaces;
using Dental_lab_Application_MVC_.Models.Service.Interface;

namespace Dental_lab_Application_MVC_.Models.Service.Implementation
{
    public class DentalServiceService : IDentalServiceService
    {
        private readonly IDentalServiceRepository _dentalServiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DentalServiceService(IDentalServiceRepository dentalServiceRepository, IUnitOfWork unitOfWork)
        {
            _dentalServiceRepository = dentalServiceRepository;
            _unitOfWork = unitOfWork;
        }

        public DentalServiceDto Create(DentalServiceCreateRequestModel createModel)
        {
            bool dentalServiceExist = _dentalServiceRepository.Exist( e => e.Code == createModel.Code || e.Name == createModel.Name);
            if (dentalServiceExist)
            {
                return new DentalServiceDto
                {
                    Message = "Dental service already exist.",
                    Status = false
                };

            }
            DentalService dentalServiceDto = new DentalService()
            {
                Name = createModel.Name,
                Description = createModel.Description,
                Code = createModel.Code,
                Cost = createModel.Cost,
            };
            _dentalServiceRepository.CreateDentalService(dentalServiceDto);
            _unitOfWork.Save();

            return new DentalServiceDto
            {
                Name = dentalServiceDto.Name,
                Description = dentalServiceDto.Description,
                Code = dentalServiceDto.Code,
                Cost = dentalServiceDto.Cost,
                Message = "Dental service created successfully.",
                Status = true
            };
        }

        public DentalServiceDto Get(Guid id)
        {
            var dentalService = _dentalServiceRepository.Get(g => g.Id == id);
            if (dentalService != null)
            {
                var dentalServiceDto = new DentalServiceDto()
                {
                    Id = id,
                    Name = dentalService.Name,
                    Description = dentalService.Description,
                    Code = dentalService.Code,
                    Cost = dentalService.Cost,
                };
                return dentalServiceDto;
            }

            return null;
        }

        public ICollection<DentalServiceDto> GetAll()
        {
            var dentalService = _dentalServiceRepository.GetAll();
            var services = dentalService.Select(d => new DentalServiceDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Code = d.Code,
                Cost = d.Cost,
                Message = "",
                Status = true
            }).ToList();
            return services;
        }

        public DentalServiceDto GetByCode(string code)
        {
            var dentalService = _dentalServiceRepository.Get(g => g.Code == code);
            if (dentalService != null)
            {
                var dentalServiceDto = new DentalServiceDto()
                {
                    Id = dentalService.Id,
                    Name = dentalService.Name,
                    Description = dentalService.Description,
                    Code = dentalService.Code,
                    Cost = dentalService.Cost,
                };
                return dentalServiceDto;
            }
            return null;
        }

        public DentalServiceDto GetByName(string name)
        {
            var dentalService = _dentalServiceRepository.Get(x => x.Name == name);
            if (dentalService != null)
            {
                var dentalServiceDto = new DentalServiceDto()
                {
                    Id= dentalService.Id,
                    Name = dentalService.Name,
                    Description = dentalService.Description,
                    Code = dentalService.Code,
                    Cost = dentalService.Cost,
                };
                return dentalServiceDto;
            }
            return null;
        }

        public DentalServiceDto Update(DentalServiceUpdateRequestModel dentalServiceUpdateModel)
        {
            var existingService = _dentalServiceRepository.Get(u => u.Code == dentalServiceUpdateModel.Code);
            if (existingService != null)
            {
                existingService.Name = dentalServiceUpdateModel.Name;
                existingService.Description = dentalServiceUpdateModel.Description;
                existingService.Cost = dentalServiceUpdateModel.Cost;

                _dentalServiceRepository.Update(existingService);
                _unitOfWork.Save();
                return new DentalServiceDto
                {
                    Name = dentalServiceUpdateModel.Name,
                    Description = dentalServiceUpdateModel.Description,
                    Cost = dentalServiceUpdateModel.Cost,
                    Message = "Dental service updated successful",
                    Status = true
                };
            }
            return null;
        }
    }
}
