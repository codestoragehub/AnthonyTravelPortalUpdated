﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AnthonyTravelPortal.UI.Areas.System
{
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
                throw new InvalidOperationException("This shouldn't be invoked in non-development environments");

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(context.Error.StackTrace, title: context.Error.Message);
        }

        [Route("/error")]
        [HttpGet]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(title: context.Error.Message);
        }
    }
}
