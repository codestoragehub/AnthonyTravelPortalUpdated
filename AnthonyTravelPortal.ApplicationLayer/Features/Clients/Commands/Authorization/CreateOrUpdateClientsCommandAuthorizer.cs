using AnthonyTravelPortal.ApplicationLayer.Authorization.Models;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.CreateClient;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.Authorization;

public class CreateOrUpdateClientCommandAuthorizer : AbstractRequestAuthorizer<CreateOrUpdateClientCommand>
{
    private readonly ICurrentUserService _currentUserService;

    public CreateOrUpdateClientCommandAuthorizer(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override void BuildPolicy(CreateOrUpdateClientCommand request)
    {
        UseRequirement(new MustOwnClientRequirement
        {
            ClientId = request.CreateClientDto.Client_ID,
            UserId = _currentUserService.UserId
        });
    }
}