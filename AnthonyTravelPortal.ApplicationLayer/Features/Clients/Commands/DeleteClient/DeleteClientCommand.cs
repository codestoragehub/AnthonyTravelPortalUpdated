using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest<BaseResponseResult<bool>>
    {
        public int? ClientId { get; set; }
    }
}
