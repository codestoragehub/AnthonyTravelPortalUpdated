using AnthonyTravelPortal.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.UI.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var permissions = context.User.Claims
                .Where(x => x.Type == CustomClaimTypes.Permission && x.Value == requirement.Permission &&
                            x.Issuer == "LOCAL AUTHORITY");

            if (permissions.Any())
                context.Succeed(requirement);
        }
    }
}
