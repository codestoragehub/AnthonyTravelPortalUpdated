using AnthonyTravelPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories
{
    public interface IInstitutionNewRepository
    {
        Task<List<Institution>> GetInstitutionListAsync();

        Task<Institution> GetInstitutionByIdAsync(int institutionId);

        Task<Institution> InsertInstitutionAsync(Institution institution);

        Task UpdateInstitutionAsync(Institution institution);

        Task DeleteInstitutionAsync(Institution institution);

    }
}
