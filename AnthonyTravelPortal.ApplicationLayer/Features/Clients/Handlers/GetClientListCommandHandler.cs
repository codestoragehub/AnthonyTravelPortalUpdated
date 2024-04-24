using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById;
using AnthonyTravelPortal.Domain.ResponseResult;
using AutoMapper;
using DisabilityInPortal.ApplicationLayer.Features.Clients.Queries.GetClientsList;
using MediatR;

namespace DisabilityInPortal.ApplicationLayer.Features.Clients.Handlers;

public class GetClientListCommandHandler : IRequestHandler<GetClientListCommand,
    BaseResponseResult<List<ClientDto>>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public GetClientListCommandHandler(IClientRepository clientRepository,
        IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponseResult<List<ClientDto>>> Handle(
        GetClientListCommand request,
        CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.GetClientListAsync();
        var clientDtos = _mapper.Map<List<ClientDto>>(clients);

        return await BaseResponseResult<List<ClientDto>>.SuccessAsync(clientDtos);
    }
}