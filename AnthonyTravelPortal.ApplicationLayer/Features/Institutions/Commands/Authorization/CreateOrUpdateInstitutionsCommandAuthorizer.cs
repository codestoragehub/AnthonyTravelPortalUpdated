using AnthonyTravelPortal.ApplicationLayer.Authorization.Models;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.Authorization;

public class CreateOrUpdateInstitutionCommandAuthorizer : AbstractRequestAuthorizer<CreateOrUpdateInstitutionCommand>
{
    private readonly ICurrentUserService _currentUserService;

    public CreateOrUpdateInstitutionCommandAuthorizer(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override void BuildPolicy(CreateOrUpdateInstitutionCommand request)
    {
        UseRequirement(new MustOwnInstitutionRequirement
        {
            InstitutionId = request.CreateInstitutionDto.Institution_ID,
            UserId = _currentUserService.UserId
        });
    }
}