using AnthonyTravelPortal.Domain.Entities.Common;
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
    [Table("Client")]
    public class Client : AuditBaseEntity
    {
        [Key]
        public int Client_ID { get; set; }
        public int Institution_ID { get; set; }

        [ForeignKey("Institution_ID")]
        public Institution? Institution { get; set; }

        [StringLength(128)]
        public string? Client_Name { get; set; }
    }
}
