using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdCommand : IRequest<BaseResponseResult<ClientDto>>
    {
        public int ClientId { get; set; }
    }
}
