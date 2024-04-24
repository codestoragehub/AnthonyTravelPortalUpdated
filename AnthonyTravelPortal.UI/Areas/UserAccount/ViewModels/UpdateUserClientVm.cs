using AnthonyTravelPortal.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AnthonyTravelPortal.UI.Areas.UserAccount.ViewModels
{
    public class UpdateUserClientVm
    {
        public string? User_ID { get; set; }
        public int Client_ID { get; set; }
        public Roles Role_ID { get; set; }
        public string? User_Name { get; set; }
        public string? User_Email { get; set; }
        public string? Phone_Number { get; set; }
        public string Password { get; set; }
        public IEnumerable<SelectListItem> Clientlist { get; set; }
    }
}
