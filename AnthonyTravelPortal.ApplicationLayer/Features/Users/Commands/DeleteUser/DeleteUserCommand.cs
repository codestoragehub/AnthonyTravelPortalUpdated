using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResponseResult<bool>>
    {
        public string User_ID { get; set; }
    }
}
