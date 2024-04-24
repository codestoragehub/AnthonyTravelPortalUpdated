using AnthonyTravelPortal.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Domain.Entities
{
    [Table("Institution")]
    public class Institution : AuditBaseEntity
    {
        [Key]
        public int Institution_ID { get; set; }
        [StringLength(128)]
        public string? Institution_Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
