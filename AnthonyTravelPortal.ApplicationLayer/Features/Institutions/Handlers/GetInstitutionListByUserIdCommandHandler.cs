using AutoMapper;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionListByUserIdCommand;
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
    public class GetInstitutionListByUserIdCommandHandler :
        IRequestHandler<GetInstitutionListByUserIdCommand, BaseResponseResult<List<InstitutionDto>>>
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IMapper _mapper;

        public GetInstitutionListByUserIdCommandHandler(IInstitutionRepository institutionRepository, IMapper mapper)
        {
            _institutionRepository = institutionRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseResult<List<InstitutionDto>>> Handle(GetInstitutionListByUserIdCommand request, 
            CancellationToken cancellationToken)
        {
            var institutionList = await _institutionRepository.GetInstitutionListAsync();
            var mappedInstitutionList = _mapper.Map<List<InstitutionDto>>(institutionList);

            return await BaseResponseResult<List<InstitutionDto>>.SuccessAsync(mappedInstitutionList);
        }
    }
}
