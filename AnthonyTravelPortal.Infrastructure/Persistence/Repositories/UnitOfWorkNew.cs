using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Infrastructure.Persistence.Repositories
{
    public class UnitOfWorkNew : IUnitOfWorkNew
    {
        private readonly AnthonyTravelPortalNewDbContext _newdbContext;

        public UnitOfWorkNew(AnthonyTravelPortalNewDbContext newdbContext)
        {
            _newdbContext = newdbContext ?? throw new ArgumentNullException(nameof(newdbContext));
        }

        public async Task<int> Commit(CancellationToken cancellationToken = default)
        {
            return await _newdbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
