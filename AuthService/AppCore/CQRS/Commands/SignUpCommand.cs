using AuthService.Domain.Dtos;
using MediatR;
using OnaxTools.Dto.Http;

namespace AuthService.AppCore.CQRS.Commands
{
    public class SignUpCommand : IRequest<GenResponse<UserCreationResponseDto>>
    {
        public SignUpCommand(UserCreationRequestDto entity)
        {

            this.Entity = entity;
        }

        public UserCreationRequestDto Entity { get; private set; }
    }
}
