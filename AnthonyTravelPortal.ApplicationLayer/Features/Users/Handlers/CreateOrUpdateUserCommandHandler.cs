using AutoMapper;
using AnthonyTravelPortal.ApplicationLayer.Common.Exception;
using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.ApplicationLayer.Features.Users.Commands.CreateUser;
using AnthonyTravelPortal.Domain.Entities;
using AnthonyTravelPortal.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Users.Handlers
{
    public class CreateOrUpdateUserCommandHandler :
        IRequestHandler<CreateOrUpdateUserCommand, BaseResponseResult<CreateOrUpdateUserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _UserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateUserCommandHandler(
            IMapper mapper,
            IUserRepository UserRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _UserRepository = UserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseResult<CreateOrUpdateUserDto>> Handle(CreateOrUpdateUserCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.CreateUserDto;

            User user;
            var wasUserAlreadyCreated = dto.ID != null;
            if (wasUserAlreadyCreated)
            {
                user = await UpdateUserAsync(dto, cancellationToken);
                return await BaseResponseResult<CreateOrUpdateUserDto>.SuccessAsync(_mapper.Map<CreateOrUpdateUserDto>(user));
            }
            else
            {
                user = await CreateNewUserAsync(dto, cancellationToken);

                var result = _mapper.Map<CreateOrUpdateUserDto>(user);
                return await BaseResponseResult<CreateOrUpdateUserDto>.SuccessAsync(result, "User created");
            }
        }

        private async Task<User> CreateNewUserAsync(CreateOrUpdateUserDto dto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(dto);
            await _UserRepository.InsertUserAsync(user);
            await _unitOfWork.Commit(cancellationToken);

            return user;
        }

        private async Task<User> UpdateUserAsync(CreateOrUpdateUserDto dto, CancellationToken cancellationToken)
        {
            var user = await _UserRepository.GetUserByUser_IDAsync(dto.User_ID);

            if (user == null)
                throw new NotFoundException(nameof(User), dto.User_ID);

            _mapper.Map(dto, user);

            await _UserRepository.UpdateUserAsync(user);
            await _unitOfWork.Commit(cancellationToken);

            return user;
        }
    }
}
