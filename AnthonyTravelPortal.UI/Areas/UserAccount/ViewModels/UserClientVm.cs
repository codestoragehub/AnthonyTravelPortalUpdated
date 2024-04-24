using AnthonyTravelPortal.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthonyTravelPortal.UI.Areas.UserAccount.ViewModels
{
    public class UserClientVm
    {
        public int? ID { get; set; }
        public string? User_ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        [Display(Name = "Client")]
        public string Client_ID { get; set; }

        [Display(Name = "First Name")]
        public string User_Name { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone_Number { get; set; }

        [Display(Name = "Role")]
        public Roles Role_ID { get; set; }
        public Roles SelectRole { get; set; }
        public string User_Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<SelectListItem> Clientlist { get; set; }

    }
}
