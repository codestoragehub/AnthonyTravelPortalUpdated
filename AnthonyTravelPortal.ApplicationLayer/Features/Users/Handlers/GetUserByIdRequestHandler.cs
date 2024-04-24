using AutoMapper;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Handlers
{
    public class GetUserByIdRequestHandler
        : IRequestHandler<GetUserByIdCommand, BaseResponseResult<UserDto>>
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;

        public GetUserByIdRequestHandler(IUserRepository UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseResult<UserDto>> Handle(
            GetUserByIdCommand request,
            CancellationToken cancellationToken)
        {
            var User = await _UserRepository.GetUserByIdAsync(request.ID);
            var UserDto = _mapper.Map<UserDto>(User);

            return await BaseResponseResult<UserDto>.SuccessAsync(UserDto);
        }
    }
}
