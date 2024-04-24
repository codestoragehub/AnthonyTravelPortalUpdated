using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.DeleteClient;
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
    public class DeleteClientCommandHandler :
       IRequestHandler<DeleteClientCommand, BaseResponseResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandler(IClientRepository clientRepository,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<BaseResponseResult<bool>> Handle(
            DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByIdAsync(request.ClientId.Value);

            if (client == null)
                throw new InvalidOperationException($"No Client is found with the given id: {request.ClientId}");

            await _clientRepository.DeleteClientAsync(client);
            var numOfRows = await _unitOfWork.Commit(cancellationToken);

            var hasDeletedItem = numOfRows > 0;
            
            return await BaseResponseResult<bool>.SuccessAsync(hasDeletedItem);
        }
    }
}
