using AuthService.Domain.Dtos;
using MediatR;
using OnaxTools.Dto.Http;

namespace AuthService.AppCore.CQRS.Commands
{
    public class UnlockAccountCommand : IRequest<GenResponse<string>>
    {
        public UnlockAccountCommand(UnclockAccountDto model)
        {

            this.Username = model.Username;
            this.ResetCode = model.ResetCode;
        }

        public string Username { get; set; }
        public string ResetCode { get; set; }
    
    }
}
