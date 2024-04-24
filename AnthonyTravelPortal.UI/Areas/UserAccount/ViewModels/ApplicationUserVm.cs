using AnthonyTravelPortal.Domain.Enums;
using AnthonyTravelPortal.UI.Areas.Client.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AnthonyTravelPortal.UI.Areas.UserAccount.ViewModels
{
    public class ApplicationUserVm
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] ProfilePicture { get; set; } = null;
        public bool IsActive { get; set; } = false;
        public int Client_ID { get; set; }
        public string Client_Name { get; set; }
        public Roles Role { get; set; }
        public List<ClientVm> Clients { get; set; }
        public IEnumerable<SelectListItem> selectListItems { get; set; }
      


    }
}
