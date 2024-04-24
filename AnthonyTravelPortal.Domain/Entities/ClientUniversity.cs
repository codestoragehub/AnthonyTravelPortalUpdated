using AnthonyTravelPortal.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Domain.Entities
{
    
    [Table("Clients_Univ")]
    public class ClientUniversity
    {
        [Key]
        public int ClientUniversity_ID { get; set; }

        
        [StringLength(255)]
        public string? ClientName { get; set; }

        public int? Client_ID { get; set; }
        [ForeignKey("Client_ID")]
        public Client? Client { get; set; }

        public float Interface_ID { get; set; }
        [StringLength(255)]

        public int? Institution_ID { get; set; }

        [ForeignKey("Institution_ID")]
        public Institution? Institution { get; set; }
        public string? University { get; set; }
    }
}
