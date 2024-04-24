using AnthonyTravelPortal.ApplicationLayer.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Authorization.Interfaces
{
    public interface IAuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
    {
        Task<AuthorizationResult> Handle(TRequirement requirement, CancellationToken cancellationToken = default);
    }
}
