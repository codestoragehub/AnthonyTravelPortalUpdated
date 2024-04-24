using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserbyUser_ID;
using AnthonyTravelPortal.Domain.ResponseResult;
using AutoMapper;
using MediatR;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Handlers
{
    public class GetUserByUser_IDRequestHandler
        : IRequestHandler<GetUserByUser_IDCommand, BaseResponseResult<UserDto>>
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;

        public GetUserByUser_IDRequestHandler(IUserRepository UserRepository, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseResult<UserDto>> Handle(
            GetUserByUser_IDCommand request,
            CancellationToken cancellationToken)
        {
            var User = await _UserRepository.GetUserByUser_IDAsync(request.User_ID);
            var UserDto = _mapper.Map<UserDto>(User);

            return await BaseResponseResult<UserDto>.SuccessAsync(UserDto);
        }
    }
}
