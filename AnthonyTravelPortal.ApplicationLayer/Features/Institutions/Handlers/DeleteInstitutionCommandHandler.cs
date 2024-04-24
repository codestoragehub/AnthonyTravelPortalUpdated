using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.DeleteInstitution;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Handlers
{
    public class DeleteInstitutionCommandHandler :
       IRequestHandler<DeleteInstitutionCommand, BaseResponseResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInstitutionCommandHandler(IInstitutionRepository institutionRepository,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _institutionRepository = institutionRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<BaseResponseResult<bool>> Handle(
            DeleteInstitutionCommand request, CancellationToken cancellationToken)
        {
            var institution = await _institutionRepository.GetInstitutionByIdAsync(request.InstitutionId);

            if (institution == null)
                throw new InvalidOperationException($"No Institution is found with the given id: {request.InstitutionId}");

            await _institutionRepository.DeleteInstitutionAsync(institution);
            var numOfRows = await _unitOfWork.Commit(cancellationToken);

            var hasDeletedItem = numOfRows > 0;
            
            return await BaseResponseResult<bool>.SuccessAsync(hasDeletedItem);
        }
    }
}
