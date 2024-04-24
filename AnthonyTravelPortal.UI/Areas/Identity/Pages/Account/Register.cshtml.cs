using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AnthonyTravelPortal.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnthonyTravelPortal.Domain.Entities;
using AnthonyTravelPortal.UI.Areas.Client.ViewModels;
using DisabilityInPortal.ApplicationLayer.Features.Clients.Queries.GetClientsList;
using Microsoft.EntityFrameworkCore;
using AnthonyTravelPortal.UI.Abstractions;
using AnthonyTravelPortal.UI.Extensions;
using AnthonyTravelPortal.Domain.Enums;

namespace AnthonyTravelPortal.UI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : BasePageModel<RegisterModel>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public IEnumerable<SelectListItem> Clientlist { get; set; }
        public class InputModel
        {
            // public string Name { get; set; }
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            public string FirstName { get; set; }

            [Display(Name = "Client")]
            public int Client_ID { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            public string  UserRole { get; set; }

            [Display(Name = "Role")]
            public Roles SelectRole { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }
            [Display(Name = "Client")]
            public IEnumerable<SelectListItem> Clientlist { get; set; }

        }


        public async Task OnGetAsync(string Id = null)
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Clientlist = await GetClientNameList();


            if (Id != null)
            {
                await LoadAsync(Id);

            }
        }

        private async Task LoadAsync(string Id)
        {
            
            var user = await _userManager.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
            var roles = await _userManager.GetRolesAsync(user);

            var roleType = Roles.InternalUser;
            bool isAdminRole = await _userManager.IsInRoleAsync(user, Roles.Admin.ToString());
            bool isUBTRole = await _userManager.IsInRoleAsync(user, Roles.UBT.ToString());
            bool isAthleticsRole = await _userManager.IsInRoleAsync(user, Roles.Athletics.ToString());
            bool isInternalUserRole = await _userManager.IsInRoleAsync(user, Roles.InternalUser.ToString());

            if (isAdminRole)
            {
                roleType = Roles.Admin;
            }
            else if (isUBTRole)
            {
                roleType = Roles.UBT;
            }
            else if (isAthleticsRole)
            {
                roleType = Roles.Athletics;
            }
            else if (isInternalUserRole)
            {
                roleType = Roles.InternalUser;
            }

            //var roleType = Roles.InternalUser;
            //if (User.IsAdmin())
            //{
            //    roleType = Roles.Admin;
            //}
            //else if (User.IsUBT())
            //{
            //    roleType = Roles.UBT;
            //}
            //else if(User.IsAthletics())
            //{
            //    roleType = Roles.Athletics;
            //}


            Input = new InputModel
            {
                FirstName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Client_ID = (int)user.Client_ID,

                ProfilePicture = user.ProfilePicture,
                Clientlist = Clientlist,
                SelectRole = roleType
            };

        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Input.SelectRole.ToString());
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private async Task<IEnumerable<SelectListItem>> GetClientNameList()
        {

            var clientList = new List<ClientVm>();
            var clientResult = await Mediator.Send(new GetClientListCommand());
            clientList = Mapper.Map<List<ClientVm>>(clientResult.Data);
            return clientList.Where(c => c.Client_Name != null)
                                  .Select(a => new SelectListItem
                                  {
                                      Value = a.Client_ID.ToString(),
                                      Text = a.Client_Name
                                  }).OrderBy(c => c.Value).ToList();

        }
    }
}
