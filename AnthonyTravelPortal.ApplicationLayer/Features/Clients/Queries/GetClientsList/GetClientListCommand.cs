using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;

namespace DisabilityInPortal.ApplicationLayer.Features.Clients.Queries.GetClientsList;

public class GetClientListCommand : IRequest<BaseResponseResult<List<ClientDto>>>
{
}