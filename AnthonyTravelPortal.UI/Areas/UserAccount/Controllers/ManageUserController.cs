using AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.CreateUser;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserbyUser_ID;
using AnthonyTravelPortal.Domain.Enums;
using AnthonyTravelPortal.Domain.Identity;
using AnthonyTravelPortal.UI.Abstractions;
using AnthonyTravelPortal.UI.Areas.Client.ViewModels;
using AnthonyTravelPortal.UI.Areas.UserAccount.ViewModels;
using DisabilityInPortal.ApplicationLayer.Features.Clients.Queries.GetClientsList;
using DisabilityInPortal.ApplicationLayer.Features.Users.Queries.GetUsersList;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.UI.Areas.UserAccount.Controllers
{
    [Area("UserAccount")]
    public class ManageUserController : BaseController<ManageUserController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<ManageUserController> _logger;
        private readonly IEmailSender _emailSender;
        public ManageUserController(UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager,
            ILogger<ManageUserController> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await GetUserClientAsync();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser([FromQuery] string UserId)
        {
            var viewModel = new UserClientVm()
            {
                User_ID = UserId
            };
            viewModel.Clientlist = await GetAllClientAsync();
            return PartialView("Shared/CreateOrUpdateUserView", viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteClient(string UserId)
        {
            var user = await _userManager.Users.Where(x => x.Id == UserId).FirstOrDefaultAsync();
            var deleteUser = await _userManager.DeleteAsync(user);
            return Ok();
        }
        private async Task<List<UserClientVm>> GetUserClientAsync()
        {
            var userList = new List<UserClientVm>();
            var result = await Mediator.Send(new GetUserListCommand());

            foreach (var item in result.Data)
            {
                var user = new UserClientVm
                {
                    User_ID = item.User_ID,
                    Role_ID = (Roles)item.Role_ID,
                    Client_ID = item.Client_ID,
                    User_Name = item.User_Name,
                    User_Email = item.User_Email,
                    Phone_Number = item.Phone_Number,
                    Clientlist = await GetAllClientAsync()
                };
                userList.Add(user);
            }

            var viewModel = Mapper.Map<List<UserClientVm>>(userList);
            return viewModel;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] string userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId);

            if (applicationUser==null)
                return NotFound();
           
            var result = await Mediator.Send(new GetUserByUser_IDCommand
            {
                User_ID = userId
            });

            var viewModel = Mapper.Map<UserClientVm>(result.Data);
            viewModel.Clientlist = await GetAllClientAsync();
            return PartialView("Shared/CreateOrUpdateUserView", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateUser([FromForm] CreateOrUpdateUserDto UserDto, CancellationToken token)
        {
            var user = new ApplicationUser
            {
                FirstName = UserDto.User_Name,
                Email = UserDto.User_Email,
                PhoneNumber = UserDto.Phone_Number,
                Client_ID = UserDto.Client_ID
            };

            var results = await _userManager.CreateAsync(user, UserDto.Password);

            if (results.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserDto.Role_ID.ToString());
                UserDto.User_ID = user.Id;
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //string returnUrl = Url.Content("~/");
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null,
                //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                var result = await Mediator.Send(new CreateOrUpdateUserCommand
                {
                    CreateUserDto = UserDto
                });
                return CreatedAtAction(nameof(GetUser), new { UserId = result.Data.User_ID });
            }
            return View();
           
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromForm] CreateOrUpdateUserDto UserDto, CancellationToken token)
        {
            var applicationUser = await _userManager.FindByIdAsync(UserDto.User_ID);

            if (applicationUser == null)
                return NotFound();
            await HandleRolesAsync(UserDto.User_ID);

            SetAppUser(applicationUser, UserDto);
            await _userManager.UpdateAsync(applicationUser);
            await _userManager.AddToRoleAsync(applicationUser, UserDto.Role_ID.ToString());

            var result = await Mediator.Send(new CreateOrUpdateUserCommand
            {
                CreateUserDto = UserDto
            });

            var viewModel = Mapper.Map<UserClientVm>(result.Data);
            viewModel.Clientlist = await GetAllClientAsync();
            return PartialView("Shared/CreateOrUpdateUserView", viewModel);
        }       

        private async Task<IEnumerable<SelectListItem>> GetAllClientAsync()
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

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var viewModel = await GetUserClientAsync();

            return PartialView("Shared/UserView", viewModel);
        }


        private void SetAppUser(ApplicationUser applicationUser, CreateOrUpdateUserDto UserDto)
        {
            applicationUser.FirstName = UserDto.User_Name;
            applicationUser.Email = UserDto.User_Email;
            applicationUser.PhoneNumber = UserDto.Phone_Number;
            applicationUser.Client_ID = UserDto.Client_ID;
        }

        private async Task HandleRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
        }
    }
}
