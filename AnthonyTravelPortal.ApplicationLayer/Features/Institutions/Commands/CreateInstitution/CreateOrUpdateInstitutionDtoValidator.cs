using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution
{
    public class CreateOrUpdateInstitutionDtoValidator : AbstractValidator<CreateOrUpdateInstitutionDto>
    {
        public CreateOrUpdateInstitutionDtoValidator()
        {
                 
        }
    }
}
