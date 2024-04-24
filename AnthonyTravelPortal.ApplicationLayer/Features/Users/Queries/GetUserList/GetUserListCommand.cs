using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;

namespace DisabilityInPortal.ApplicationLayer.Features.Users.Queries.GetUsersList;

public class GetUserListCommand : IRequest<BaseResponseResult<List<UserDto>>>
{
}