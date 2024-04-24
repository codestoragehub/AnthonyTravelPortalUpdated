using AutoMapper;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById;
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
    public class GetInstitutionByIdRequestHandler
        : IRequestHandler<GetInstitutionByIdCommand, BaseResponseResult<InstitutionDto>>
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IMapper _mapper;

        public GetInstitutionByIdRequestHandler(IInstitutionRepository institutionRepository, IMapper mapper)
        {
            _institutionRepository = institutionRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseResult<InstitutionDto>> Handle(
            GetInstitutionByIdCommand request,
            CancellationToken cancellationToken)
        {
            var institution = await _institutionRepository.GetInstitutionByIdAsync(request.Institution_ID);
            var institutionDto = _mapper.Map<InstitutionDto>(institution);

            return await BaseResponseResult<InstitutionDto>.SuccessAsync(institutionDto);
        }
    }
}
