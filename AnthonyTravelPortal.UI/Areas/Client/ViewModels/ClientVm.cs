using AnthonyTravelPortal.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnthonyTravelPortal.UI.Areas.Institution.ViewModels;
using AnthonyTravelPortal.UI.Areas.UserAccount.ViewModels;

namespace AnthonyTravelPortal.UI.Areas.Client.ViewModels
{
    public class ClientVm
    {
        public int? Client_ID { get; set; }

        [Display(Name = "Institution")]
        public int Institution_ID { get; set; }
        public InstitutionVm Institution { get; set; }

        public IEnumerable<SelectListItem> Institutionlist { get; set; }
        

        [StringLength(250)]
        [Display(Name = "Client Name")]
        public string Client_Name { get; set; }     

        public List<ApplicationUserVm> ApplicationUsers { get; set; }
    }
}
