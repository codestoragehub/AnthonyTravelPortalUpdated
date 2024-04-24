using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserbyUser_ID
{
    public class GetUserByUser_IDCommand : IRequest<BaseResponseResult<UserDto>>
    {
        public string User_ID { get; set; }
    }
}
