using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AnthonyTravelPortal.Domain.Entities.Common;

namespace AnthonyTravelPortal.Domain.Entities
{
    [Table("Role")]
    public class Role 
    {
        [Key]
        public string Role_ID { get; set; }
        [StringLength(128)]
        public string Role_Name { get; set; }
    }
}
