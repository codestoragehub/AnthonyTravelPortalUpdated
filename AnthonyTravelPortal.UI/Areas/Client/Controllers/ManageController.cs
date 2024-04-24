using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.CreateClient;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.DeleteClient;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientListByUserIdCommand;
using AnthonyTravelPortal.Domain.Identity;
using AnthonyTravelPortal.UI.Abstractions;
using AnthonyTravelPortal.UI.Areas.Client.ViewModels;
using AnthonyTravelPortal.UI.Areas.Institution.ViewModels;
using DisabilityInPortal.ApplicationLayer.Features.Clients.Queries.GetClientsList;
using DisabilityInPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionsList;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.UI.Areas.Client.Controllers
{
    [Area("Client")]
    public class ManageController : BaseController<ManageController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ManageController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var viewModel = await GetAllClientAsync(UserId);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddClient()
        {
            var viewModel = new ClientVm();

            viewModel.Institutionlist = await GetInstitutionNameList();
            return PartialView("Shared/CreateOrEditClientView", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetClient([FromQuery] int ClientId)
        {
            var result = await Mediator.Send(new GetClientByIdCommand
            {
                ClientId = ClientId
            });

            var viewModel = Mapper.Map<ClientVm>(result.Data);
            viewModel.Institutionlist = await GetInstitutionNameList();
            return PartialView("Shared/CreateOrEditClientView", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateClient([FromForm] CreateOrUpdateClientDto ClientDto, CancellationToken token)
        {
            var result = await Mediator.Send(new CreateOrUpdateClientCommand
            {
                CreateClientDto = ClientDto,
            });

            return CreatedAtAction(nameof(GetClient), new { ClientId = result.Data.Client_ID });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient([FromForm] CreateOrUpdateClientDto ClientDto, CancellationToken token)
        {
            var result = await Mediator.Send(new CreateOrUpdateClientCommand
            {
                CreateClientDto = ClientDto
            });

            var viewModel = Mapper.Map<ClientVm>(result.Data);
            viewModel.Institutionlist = await GetInstitutionNameList();
            return PartialView("Shared/CreateOrEditClientView", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient([FromQuery] int ClientId)
        {
            await Mediator.Send(new DeleteClientCommand
            {
                ClientId = ClientId
            });

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var viewModel = await GetAllClientAsync(UserId);

            return PartialView("Shared/ClientView", viewModel);
        }


        private async Task<List<ClientVm>> GetAllClientAsync(string userId)
        {
            var clientRes = await Mediator.Send(new GetClientListCommand());
            var Clients = Mapper.Map<List<ClientVm>>(clientRes.Data);

            return Clients;
        }

        private async Task<IEnumerable<SelectListItem>> GetInstitutionNameList()
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



    }
}
