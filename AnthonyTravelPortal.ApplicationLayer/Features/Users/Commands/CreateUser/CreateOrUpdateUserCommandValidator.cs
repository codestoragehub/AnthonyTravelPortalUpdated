using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.CreateUser
{
    public class CreateOrUpdateUserCommandValidator : AbstractValidator<CreateOrUpdateUserCommand>
    {
        public CreateOrUpdateUserCommandValidator()
        {
            RuleFor(a => a.CreateUserDto)
                .SetValidator(new CreateOrUpdateUserDtoValidator());
        }
    }
}
