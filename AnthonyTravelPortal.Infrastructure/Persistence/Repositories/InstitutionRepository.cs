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
    internal class InstitutionRepository : IInstitutionRepository
    {
        private readonly IRepositoryAsync<Institution> _repository;

        public InstitutionRepository(IRepositoryAsync<Institution> repository)
        {
            _repository = repository;
        }
        private IQueryable<Institution> Institutions => _repository.Entities;

        public async Task DeleteInstitutionAsync(Institution institution)
        {
            await _repository.DeleteAsync(institution);
        }

        public async Task<Institution> GetInstitutionByIdAsync(int institutionId)
        {
            return await _repository.Entities
               .FirstOrDefaultAsync(c => c.Institution_ID == institutionId);
        }

        public async Task<List<Institution>> GetInstitutionListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }
      
        public async Task<Institution> InsertInstitutionAsync(Institution institution)
        {
            return await _repository.AddAsync(institution);
        }

        public async Task UpdateInstitutionAsync(Institution institution)
        {
            await _repository.UpdateAsync(institution);
        }
    }
}
