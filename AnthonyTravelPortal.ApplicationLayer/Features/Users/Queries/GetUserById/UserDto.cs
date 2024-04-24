using AnthonyTravelPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById
{
    public class UserDto
    {
        public int? ID { get; set; }
        public string? User_ID { get; set; }
        public string? Client_ID { get; set; }
        public Roles? Role_ID { get; set; }
        public string? User_Name { get; set; }
        public string? User_Email { get; set; }
        public string? Phone_Number { get; set; }

    }
}
