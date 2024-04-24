using AnthonyTravelPortal.Domain.Enums;
using AnthonyTravelPortal.Domain.Identity;
using AnthonyTravelPortal.UI.Abstractions;
using AnthonyTravelPortal.UI.Areas.Client.ViewModels;
using AnthonyTravelPortal.UI.Areas.Dashboard.ViewModels;
using AnthonyTravelPortal.UI.Areas.Institution.ViewModels;
using AnthonyTravelPortal.UI.Areas.UserAccount.ViewModels;
using AutoMapper;
using DisabilityInPortal.ApplicationLayer.Features.Clients.Queries.GetClientsList;
using DisabilityInPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionsList;
using DisabilityInPortal.ApplicationLayer.Features.Users.Queries.GetUsersList;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.UI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseController<HomeController>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index([FromQuery] string message = null)
        {
            
            var viewModel = new DashBoardSettingsVm
            {
                Clientlist = await GetAllClientAsync(),
                Institutionlist = await GetInstitutionNameList(),
                Userlist = await GetUserClientAsync()
            };
            return View(viewModel);
        }

        public async Task<IEnumerable<SelectListItem>> GetInstitutionNameList()
        {
            var institutionList = new List<InstitutionVm>();
            var institutionResult = await Mediator.Send(new GetInstitutionListCommand());
            institutionList = Mapper.Map<List<InstitutionVm>>(institutionResult.Data);
            return institutionList.Where(c => c.Institution_Name != null)
                                  .Select(a => new SelectListItem
                                  {
                                      Value = a.Institution_ID.ToString(),
                                      Text = a.Institution_Name
                                  }).OrderBy(c => c.Value).ToList();
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

    }
}
