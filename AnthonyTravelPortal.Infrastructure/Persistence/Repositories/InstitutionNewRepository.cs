using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Infrastructure.Persistence.Repositories
{
    internal class InstitutionNewRepository : IInstitutionNewRepository
    {
        private readonly IRepositoryAsyncNew<Institution> _newrepository;

        public InstitutionNewRepository(IRepositoryAsyncNew<Institution> newrepository)
        {
            _newrepository = newrepository;
        }
        private IQueryable<Institution> Institutions => _newrepository.Entities;

        public async Task DeleteInstitutionAsync(Institution institution)
        {
            await _newrepository.DeleteAsync(institution);
        }

        public async Task<Institution> GetInstitutionByIdAsync(int institutionId)
        {
            return await _newrepository.Entities
               .FirstOrDefaultAsync(c => c.Institution_ID == institutionId);
        }

        public async Task<List<Institution>> GetInstitutionListAsync()
        {
            return await _newrepository.Entities.ToListAsync();
        }

        public async Task<Institution> InsertInstitutionAsync(Institution institution)
        {
            return await _newrepository.AddAsync(institution);
        }

        public async Task UpdateInstitutionAsync(Institution institution)
        {
            await _newrepository.UpdateAsync(institution);
        }
    }
}

