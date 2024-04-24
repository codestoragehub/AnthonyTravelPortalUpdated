using AnthonyTravelPortal.Domain.Identity;
using AnthonyTravelPortal.UI.Areas.UserAccount.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthonyTravelPortal.UI.Areas.Dashboard.ViewModels
{
    public class DashBoardSettingsVm
    {
        [Display(Name = "Institution Name")]
        public IEnumerable<SelectListItem> Institutionlist { get; set; }
        public IEnumerable<SelectListItem> Clientlist { get; set; }

        public List<UserClientVm> Userlist { get; set; }

        public int ClientId { get; set; }

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
    }
}
