using AutoMapper;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientListByUserIdCommand;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Handlers
{
    public class GetClientListByUserIdCommandHandler :
        IRequestHandler<GetClientListByUserIdCommand, BaseResponseResult<List<ClientDto>>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientListByUserIdCommandHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseResult<List<ClientDto>>> Handle(GetClientListByUserIdCommand request, 
            CancellationToken cancellationToken)
        {
            var clientList = await _clientRepository.GetClientListAsync();
            var mappedClientList = _mapper.Map<List<ClientDto>>(clientList);

            return await BaseResponseResult<List<ClientDto>>.SuccessAsync(mappedClientList);
        }
    }
}
