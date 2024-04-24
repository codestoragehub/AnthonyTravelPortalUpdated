using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Services;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.DeleteInstitution;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionListByUserIdCommand;
using AnthonyTravelPortal.Domain.Identity;
using AnthonyTravelPortal.UI.Abstractions;
using AnthonyTravelPortal.UI.Areas.Institution.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.UI.Areas.Institution.Controllers
{
    [Area("Institution")]
    public class ManageController : BaseController<ManageController>
    {
        private readonly IAnthonyCloneServices _anthonyCloneServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public ManageController(UserManager<ApplicationUser> userManager, IAnthonyCloneServices anthonyCloneServices)
        {
            _userManager = userManager;
            _anthonyCloneServices = anthonyCloneServices;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = await GetAllInstitutionAsync(UserId);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddInstitution()
        {
            
            var viewModel = new InstitutionVm
            {
                UserId = UserId
            };

            return PartialView("Shared/CreateOrEditInstitutionView", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetInstitution([FromQuery] int InstitutionId)
        {
            var result = await Mediator.Send(new GetInstitutionByIdCommand
            {
                Institution_ID = InstitutionId
            });

            var viewModel = Mapper.Map<InstitutionVm>(result.Data);
            return PartialView("Shared/CreateOrEditInstitutionView", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateInstitution([FromForm] CreateOrUpdateInstitutionDto InstitutionDto, CancellationToken token)
        {
            var result = await Mediator.Send(new CreateOrUpdateInstitutionCommand
            {
                CreateInstitutionDto = InstitutionDto,
            }) ;

            return CreatedAtAction(nameof(GetInstitution), new { InstitutionId = result.Data.Institution_ID });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInstitution([FromForm] CreateOrUpdateInstitutionDto InstitutionDto, CancellationToken token)
        {
            var result = await Mediator.Send(new CreateOrUpdateInstitutionCommand
            {
                CreateInstitutionDto = InstitutionDto
            });

            var viewModel = Mapper.Map<InstitutionVm>(result.Data);

            return PartialView("Shared/CreateOrEditInstitutionView", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInstitution([FromQuery] int InstitutionId)
        {
            await Mediator.Send(new DeleteInstitutionCommand
            {
                InstitutionId = InstitutionId
            });

            return Ok();
        }

       
        [HttpGet]
        public async Task<IActionResult> GetInstitutions()
        {
            var viewModel = await GetAllInstitutionAsync(UserId);

            return PartialView("Shared/InstitutionView", viewModel);
        }

        private async Task<List<InstitutionVm>> GetAllInstitutionAsync(string userId)
        {
            var institutionRes = await Mediator.Send(new GetInstitutionListByUserIdCommand
            {
                Id = userId
            });
            var Institutions = Mapper.Map<List<InstitutionVm>>(institutionRes.Data);
            return Institutions;
        }

        public async Task<IActionResult> CreateNewInstitution()
        {
            var result = _anthonyCloneServices.CreateInstitutionAsync();
            return View();
        }

    }
}
