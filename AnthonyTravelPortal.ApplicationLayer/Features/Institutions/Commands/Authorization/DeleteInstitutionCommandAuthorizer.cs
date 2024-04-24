using AnthonyTravelPortal.ApplicationLayer.Authorization.Models;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.DeleteInstitution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.Authorization
{
    public class DeleteInstitutionCommandAuthorizer : AbstractRequestAuthorizer<DeleteInstitutionCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteInstitutionCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(DeleteInstitutionCommand request)
        {
            UseRequirement(new MustOwnInstitutionRequirement
            {
                InstitutionId = request.InstitutionId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
