using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;

namespace DisabilityInPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionsList;

public class GetInstitutionListCommand : IRequest<BaseResponseResult<List<InstitutionDto>>>
{
}