using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById
{
    public class GetUserByIdCommand : IRequest<BaseResponseResult<UserDto>>
    {
        public int ID { get; set; }
    }
}
