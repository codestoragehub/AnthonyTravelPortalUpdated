using AnthonyTravelPortal.Domain.Entities;
using AnthonyTravelPortal.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Contexts
{
    public interface IAnthonyTravelPortalDbContext
    {
        IDbConnection Connection { get; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Institution> Institutions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClientUniversity> ClientUniversities { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
