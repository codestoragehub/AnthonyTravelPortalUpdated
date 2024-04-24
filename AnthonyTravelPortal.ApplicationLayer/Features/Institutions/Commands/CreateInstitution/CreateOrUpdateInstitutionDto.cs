using AnthonyTravelPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution
{
    public class CreateOrUpdateInstitutionDto
    {
        public int? Institution_ID { get; set; }
        public string? Institution_Name { get; set; }
        public string? userId { get; set; }
    }
}
