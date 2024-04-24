using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.CreateClient
{
    public class CreateOrUpdateClientDtoValidator : AbstractValidator<CreateOrUpdateClientDto>
    {
        public CreateOrUpdateClientDtoValidator()
        {
                 
        }
    }
}
