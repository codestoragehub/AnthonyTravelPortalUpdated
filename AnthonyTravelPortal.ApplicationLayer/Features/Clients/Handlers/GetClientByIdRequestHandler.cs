using AutoMapper;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById;
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
    public class GetClientByIdRequestHandler
        : IRequestHandler<GetClientByIdCommand, BaseResponseResult<ClientDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdRequestHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseResult<ClientDto>> Handle(
            GetClientByIdCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByIdAsync(request.ClientId);
            var clientDto = _mapper.Map<ClientDto>(client);

            return await BaseResponseResult<ClientDto>.SuccessAsync(clientDto);
        }
    }
}
