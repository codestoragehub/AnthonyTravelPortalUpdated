using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.DeleteUser;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Handlers
{
    public class DeleteUserCommandHandler :
       IRequestHandler<DeleteUserCommand, BaseResponseResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _UserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommandHandler(IUserRepository UserRepository,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _UserRepository = UserRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<BaseResponseResult<bool>> Handle(
            DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var User = await _UserRepository.GetUserByUser_IDAsync(request.User_ID);

            if (User == null)
                throw new InvalidOperationException($"No User is found with the given id: {request.User_ID}");

            await _UserRepository.DeleteUserAsync(User);
            var numOfRows = await _unitOfWork.Commit(cancellationToken);

            var hasDeletedItem = numOfRows > 0;
            
            return await BaseResponseResult<bool>.SuccessAsync(hasDeletedItem);
        }
    }
}
