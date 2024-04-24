using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById
{
    public class GetInstitutionByIdCommand : IRequest<BaseResponseResult<InstitutionDto>>
    {
        public int Institution_ID { get; set; }
    }
}
