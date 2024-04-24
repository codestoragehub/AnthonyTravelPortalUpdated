using AnthonyTravelPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }       
        public byte[]? ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;
        public int Client_ID { get; set; }
    }
}
