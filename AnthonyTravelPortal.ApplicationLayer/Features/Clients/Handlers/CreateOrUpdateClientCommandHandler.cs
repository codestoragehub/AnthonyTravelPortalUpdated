using AutoMapper;
using AnthonyTravelPortal.ApplicationLayer.Common.Exception;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.CreateClient;
using AnthonyTravelPortal.Domain.Entities;
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
    public class CreateOrUpdateClientCommandHandler :
        IRequestHandler<CreateOrUpdateClientCommand, BaseResponseResult<CreateOrUpdateClientDto>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _ClientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateClientCommandHandler(
            IMapper mapper,
            IClientRepository ClientRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _ClientRepository = ClientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseResult<CreateOrUpdateClientDto>> Handle(CreateOrUpdateClientCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.CreateClientDto;

            Client client;
            var wasClientAlreadyCreated = dto.Client_ID != null && dto.Client_ID.Value > 0; ;
            if (wasClientAlreadyCreated)
            {
                client = await UpdateClientAsync(dto, cancellationToken);
                return await BaseResponseResult<CreateOrUpdateClientDto>.SuccessAsync(_mapper.Map<CreateOrUpdateClientDto>(client));
            }
            else
            {
                client = await CreateNewClientAsync(dto, cancellationToken);
                var result = _mapper.Map<CreateOrUpdateClientDto>(client);
                return await BaseResponseResult<CreateOrUpdateClientDto>.SuccessAsync(result, "Client created");
            }
        }

        private async Task<Client> CreateNewClientAsync(CreateOrUpdateClientDto dto, CancellationToken cancellationToken)
        {
            var client = _mapper.Map<Client>(dto);
            await _ClientRepository.InsertClientAsync(client);
            await _unitOfWork.Commit(cancellationToken);

            return client;
        }

        private async Task<Client> UpdateClientAsync(CreateOrUpdateClientDto dto, CancellationToken cancellationToken)
        {
            var client = await _ClientRepository.GetClientByIdAsync(dto.Client_ID.Value);

            if (client == null)
                throw new NotFoundException(nameof(Client), dto.Client_ID);

            _mapper.Map(dto, client);

            await _ClientRepository.UpdateClientAsync(client);
            await _unitOfWork.Commit(cancellationToken);

            return client;
        }
    }
}
