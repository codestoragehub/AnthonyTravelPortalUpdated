using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Common.Services
{
    public class AnthonyCloneServices : IAnthonyCloneServices
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IInstitutionNewRepository _institutionNewRepository;
        private readonly ILogger<AnthonyCloneServices> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkNew _unitOfWorkNew;
        private readonly IMapper _mapper;
        public AnthonyCloneServices(IInstitutionRepository institutionRepository,
            IInstitutionNewRepository institutionNewRepository,
            ILogger<AnthonyCloneServices> logger, IUnitOfWork unitOfWork, IMapper mapper, IUnitOfWorkNew unitOfWorkNew)
        {
            _institutionRepository = institutionRepository;
            _institutionNewRepository = institutionNewRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitOfWorkNew = unitOfWorkNew;
        }

        public async Task<Institution> CreateInstitutionAsync()
        {
            var existingInstitution = await _institutionRepository.GetInstitutionListAsync();

            return await UtiliseNewInstitutionAsync(existingInstitution);
        }

        private async Task<Institution> UtiliseNewInstitutionAsync(List<Institution> existingInstitution)
        {
            var sInstitutionList = new Institution();
            foreach (var item in existingInstitution)
            {
                var newInstitution = new Institution
                {
                    //Institution_ID = item.Institution_ID,
                    Institution_Name = item.Institution_Name,
                    IsActive = item.IsActive,
                };
                await _institutionNewRepository.InsertInstitutionAsync(newInstitution);
                await _unitOfWorkNew.Commit();
            }

            return sInstitutionList;
        }
    }
}
