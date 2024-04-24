using AnthonyTravelPortal.Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthonyTravelPortal.UI.Areas.Institution.ViewModels
{
    public class InstitutionVm
    {
        public int? Institution_ID { get; set; }

        [Display(Name = "Institution Name")]
        public string Institution_Name { get; set; }
        public string UserId { get; set; }
    }
}
