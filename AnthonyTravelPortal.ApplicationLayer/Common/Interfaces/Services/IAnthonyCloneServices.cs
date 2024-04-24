using AnthonyTravelPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services
{
    public interface IAnthonyCloneServices
    {
        Task<Institution> CreateInstitutionAsync();
    }
}
