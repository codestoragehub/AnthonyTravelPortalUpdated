using AnthonyTravelPortal.Domain.ResponseResult;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AnthonyTravelPortal.UI.Abstractions
{
    public abstract class BaseController<T> : Controller
    {
        private ILogger<T> _loggerInstance;
        private IMapper _mapperInstance;
        private IMediator _mediatorInstance;

        protected BaseController(ILogger<T> loggerInstance, IMapper mapperInstance, IMediator mediatorInstance
           )
        {
            _loggerInstance = loggerInstance;
            _mapperInstance = mapperInstance;
            _mediatorInstance = mediatorInstance;
        }

        protected BaseController()
        {
        }

        public string UserId => HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public string Username => HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> Logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();

     
        protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();

        protected bool SetModelErrors<TR>(BaseResponseResult<TR> model)
        {
            if (model.Succeeded)
                return false;

            ModelState.AddModelError("Error", "An error occured.");
            model.ValidationResult.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });

            return true;
        }
    }
}
