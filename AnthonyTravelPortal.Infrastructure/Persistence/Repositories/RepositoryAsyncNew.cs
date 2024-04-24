using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Infrastructure.Persistence.Repositories
{
    public class RepositoryAsyncNew<T> : IRepositoryAsyncNew<T> where T : class
    {
        private readonly AnthonyTravelPortalNewDbContext _newdbContext;
        public RepositoryAsyncNew( AnthonyTravelPortalNewDbContext newdbContext)
        {
            _newdbContext = newdbContext;
        }

        public IQueryable<T> Entities => _newdbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _newdbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _newdbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }     
        
        public async Task<List<T>> GetAllAsync()
        {
            return await _newdbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _newdbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _newdbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _newdbContext.Entry(entity).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<T> entities)
        {
            _newdbContext.Set<T>().AddRange(entities);
            return Task.CompletedTask;
        }
        public Task RemoveRange(IEnumerable<T> entities)
        {
            _newdbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
