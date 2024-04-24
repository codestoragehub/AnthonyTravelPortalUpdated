using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById;
using AnthonyTravelPortal.Domain.ResponseResult;
using AutoMapper;
using DisabilityInPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionsList;
using MediatR;

namespace DisabilityInPortal.ApplicationLayer.Features.Institutions.Handlers;

public class GetInstitutionListCommandHandler : IRequestHandler<GetInstitutionListCommand,
    BaseResponseResult<List<InstitutionDto>>>
{
    private readonly IInstitutionRepository _institutionRepository;
    private readonly IMapper _mapper;

    public GetInstitutionListCommandHandler(IInstitutionRepository institutionRepository,
        IMapper mapper)
    {
        _institutionRepository = institutionRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponseResult<List<InstitutionDto>>> Handle(
        GetInstitutionListCommand request,
        CancellationToken cancellationToken)
    {
        var institutions = await _institutionRepository.GetInstitutionListAsync();
        var institutionDtos = _mapper.Map<List<InstitutionDto>>(institutions);

        return await BaseResponseResult<List<InstitutionDto>>.SuccessAsync(institutionDtos);
    }
}