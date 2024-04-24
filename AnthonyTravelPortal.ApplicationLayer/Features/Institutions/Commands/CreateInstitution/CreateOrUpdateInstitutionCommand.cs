using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution
{
    public class CreateOrUpdateInstitutionCommand : IRequest<BaseResponseResult<CreateOrUpdateInstitutionDto>>
    {
        public CreateOrUpdateInstitutionDto? CreateInstitutionDto { get; init; }
    }
}
