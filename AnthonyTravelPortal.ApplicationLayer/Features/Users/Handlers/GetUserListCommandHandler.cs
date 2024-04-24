using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById;
using AnthonyTravelPortal.Domain.ResponseResult;
using AutoMapper;
using DisabilityInPortal.ApplicationLayer.Features.Users.Queries.GetUsersList;
using MediatR;

namespace DisabilityInPortal.ApplicationLayer.Features.Users.Handlers;

public class GetUserListCommandHandler : IRequestHandler<GetUserListCommand,
    BaseResponseResult<List<UserDto>>>
{
    private readonly IUserRepository _UserRepository;
    private readonly IMapper _mapper;

    public GetUserListCommandHandler(IUserRepository UserRepository,
        IMapper mapper)
    {
        _UserRepository = UserRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponseResult<List<UserDto>>> Handle(
        GetUserListCommand request,
        CancellationToken cancellationToken)
    {
        var Users = await _UserRepository.GetUserListAsync();
        var UserDtos = _mapper.Map<List<UserDto>>(Users);

        return await BaseResponseResult<List<UserDto>>.SuccessAsync(UserDtos);
    }
}