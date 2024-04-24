using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.CreateUser
{
    public class CreateOrUpdateUserCommand : IRequest<BaseResponseResult<CreateOrUpdateUserDto>>
    {
        public CreateOrUpdateUserDto? CreateUserDto { get; init; }
    }
}
