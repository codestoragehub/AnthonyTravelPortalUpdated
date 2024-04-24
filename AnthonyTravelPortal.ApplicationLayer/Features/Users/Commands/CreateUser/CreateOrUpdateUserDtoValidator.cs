using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.CreateUser
{
    public class CreateOrUpdateUserDtoValidator : AbstractValidator<CreateOrUpdateUserDto>
    {
        public CreateOrUpdateUserDtoValidator()
        {
                 
        }
    }
}
