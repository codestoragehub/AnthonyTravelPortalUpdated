using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.CreateClient;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.CreateUser;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById;
using AnthonyTravelPortal.Domain.Entities;
using AnthonyTravelPortal.Domain.Identity;
using AutoMapper;

namespace AnthonyTravelPortal.UI.Profiles
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Institution, CreateOrUpdateInstitutionDto>();
            CreateMap<Institution, InstitutionDto>();

            CreateMap<Client, CreateOrUpdateClientDto>();
            CreateMap<Client, ClientDto>();

            CreateMap<User, CreateOrUpdateUserDto>();
            CreateMap<User, UserDto>();
        }
    }
}
