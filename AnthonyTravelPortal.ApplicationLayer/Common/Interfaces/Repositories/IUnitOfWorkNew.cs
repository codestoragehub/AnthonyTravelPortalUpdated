using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories
{
    public interface IUnitOfWorkNew
    {
        Task<int> Commit(CancellationToken cancellationToken = default);
    }

}
