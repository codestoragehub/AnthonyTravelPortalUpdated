using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.CreateClient
{
    public class CreateOrUpdateClientCommand : IRequest<BaseResponseResult<CreateOrUpdateClientDto>>
    {
        public CreateOrUpdateClientDto? CreateClientDto { get; init; }
    }
}
