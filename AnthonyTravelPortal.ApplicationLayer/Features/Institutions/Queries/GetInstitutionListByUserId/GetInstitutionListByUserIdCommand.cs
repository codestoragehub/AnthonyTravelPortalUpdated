using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionListByUserIdCommand
{
    public class GetInstitutionListByUserIdCommand : IRequest<BaseResponseResult<List<InstitutionDto>>>
    {
        public string Id { get; set; }
    }
}
