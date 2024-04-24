using AnthonyTravelPortal.Domain.Entities.Common;
using AnthonyTravelPortal.Domain.Enums;
using AnthonyTravelPortal.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Domain.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public ApplicationUser ApplicationUser { get; set; }

        [StringLength(16)]
        public string Client_ID { get; set; }

        public Roles Role_ID { get; set; }
        public string? User_Name { get; set; }
        [StringLength(128)]
        public string? User_Email { get; set; }
        [StringLength(50)]
        public string? Phone_Number { get; set; }
    }
}
