using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.CreateClient;
using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Commands.CreateInstitution;
using AnthonyTravelPortal.ApplicationLayer.Features.Institutions.Queries.GetInstitutionById;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.CreateUser;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Queries.GetUserById;
using AnthonyTravelPortal.Domain.Entities;
using AnthonyTravelPortal.Domain.Identity;
using AnthonyTravelPortal.UI.Areas.Client.ViewModels;
using AnthonyTravelPortal.UI.Areas.Institution.ViewModels;
using AnthonyTravelPortal.UI.Areas.UserAccount.ViewModels;
using AutoMapper;

namespace AnthonyTravelPortal.UI.Profiles
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<CreateOrUpdateInstitutionDto , InstitutionVm>().ReverseMap();
            CreateMap<InstitutionDto, InstitutionVm>();
            CreateMap<CreateOrUpdateInstitutionDto, Institution>();

            CreateMap<CreateOrUpdateClientDto, ClientVm>().ReverseMap();
            CreateMap<ClientDto, ClientVm>();
            CreateMap<CreateOrUpdateClientDto, Client>();

            CreateMap<CreateOrUpdateUserDto, UserClientVm>().ReverseMap();
            CreateMap<UserDto, UserClientVm>();
            CreateMap<CreateOrUpdateUserDto, User>();

        }
    }
}
