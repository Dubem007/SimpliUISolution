using AuthService.Domain.Dtos;
using MediatR;
using OnaxTools.Dto.Http;

namespace AuthService.AppCore.CQRS.Commands
{
    public class ChangePasswordCommand : IRequest<GenResponse<string>>
    {
        public ChangePasswordCommand(ChangePasswordDto model)
        {

            this.Username = model.Username;
            this.OldPassword = model.OldPassword;
            this.NewPassword = model.NewPassword;
            this.ConfirmPassword = model.ConfirmPassword;
        }

        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
