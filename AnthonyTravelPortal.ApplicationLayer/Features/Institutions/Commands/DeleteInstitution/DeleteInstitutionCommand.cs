using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.DeleteInstitution
{
    public class DeleteInstitutionCommand : IRequest<BaseResponseResult<bool>>
    {
        public int InstitutionId { get; set; }
    }
}
