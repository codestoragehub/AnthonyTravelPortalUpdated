using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientListByUserIdCommand
{
    public class GetClientListByUserIdCommand : IRequest<BaseResponseResult<List<ClientDto>>>
    {
        public string Id { get; set; }
    }
}
