using AutoMapper;
using AnthonyTravelPortal.ApplicationLayer.Common.Exception;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution;
using AnthonyTravelPortal.Domain.Entities;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Handlers
{
    public class CreateOrUpdateInstitutionCommandHandler :
        IRequestHandler<CreateOrUpdateInstitutionCommand, BaseResponseResult<CreateOrUpdateInstitutionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IInstitutionRepository _InstitutionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateInstitutionCommandHandler(
            IMapper mapper,
            IInstitutionRepository InstitutionRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _InstitutionRepository = InstitutionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseResult<CreateOrUpdateInstitutionDto>> Handle(CreateOrUpdateInstitutionCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.CreateInstitutionDto;

            Institution institution;
            var wasInstitutionAlreadyCreated = dto.Institution_ID != null && dto.Institution_ID.Value > 0;
            if (wasInstitutionAlreadyCreated)
            {
                institution = await UpdateInstitutionAsync(dto, cancellationToken);
                return await BaseResponseResult<CreateOrUpdateInstitutionDto>.SuccessAsync(_mapper.Map<CreateOrUpdateInstitutionDto>(institution));
            }
            else
            {
                institution = await CreateNewInstitutionAsync(dto, cancellationToken);
                var result = _mapper.Map<CreateOrUpdateInstitutionDto>(institution);
                return await BaseResponseResult<CreateOrUpdateInstitutionDto>.SuccessAsync(result, "Institution created");
            }
        }

        private async Task<Institution> CreateNewInstitutionAsync(CreateOrUpdateInstitutionDto dto, CancellationToken cancellationToken)
        {
            var institution = _mapper.Map<Institution>(dto);
            await _InstitutionRepository.InsertInstitutionAsync(institution);
            await _unitOfWork.Commit(cancellationToken);

            return institution;
        }

        private async Task<Institution> UpdateInstitutionAsync(CreateOrUpdateInstitutionDto dto, CancellationToken cancellationToken)
        {
            var institution = await _InstitutionRepository.GetInstitutionByIdAsync(dto.Institution_ID.Value);

            if (institution == null)
                throw new NotFoundException(nameof(Institution), dto.Institution_ID);

            _mapper.Map(dto, institution);

            await _InstitutionRepository.UpdateInstitutionAsync(institution);
            await _unitOfWork.Commit(cancellationToken);

            return institution;
        }
    }
}
