using AnthonyTravelPortal.Domain.Enums;
using System.Security.Claims;

namespace AnthonyTravelPortal.UI.Extensions
{
    public static class IdentityExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.Admin.ToString());
        }

        public static bool IsAthletics(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.Athletics.ToString()); 
        }

        public static bool IsUBT(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.UBT.ToString());
        }

        public static bool IsInternalUser(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.InternalUser.ToString());
        }
    }
}
