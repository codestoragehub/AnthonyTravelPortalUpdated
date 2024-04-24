using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution
{
    public class CreateOrUpdateInstitutionCommandValidator : AbstractValidator<CreateOrUpdateInstitutionCommand>
    {
        public CreateOrUpdateInstitutionCommandValidator()
        {
            RuleFor(a => a.CreateInstitutionDto)
                .SetValidator(new CreateOrUpdateInstitutionDtoValidator());
        }
    }
}
