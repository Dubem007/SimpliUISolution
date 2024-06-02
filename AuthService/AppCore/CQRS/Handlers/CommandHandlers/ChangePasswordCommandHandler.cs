using AuthService.AppCore.CQRS.Commands;
using AuthService.AppCore.Interfaces;
using AuthService.Domain.Dtos;
using MediatR;
using OnaxTools.Dto.Http;

namespace AuthService.AppCore.CQRS.Handlers.CommandHandlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, GenResponse<string>>
    {
        private readonly ILogger<ChangePasswordCommand> logger;
        private readonly IAuthRepository _authRepo;
        private readonly IMediator mediator;

        public ChangePasswordCommandHandler(ILogger<ChangePasswordCommand> logger, IAuthRepository authRepo, IMediator mediator)
        {
            this.logger = logger;
            _authRepo = authRepo;
            this.mediator = mediator;
        }
        public async Task<GenResponse<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var passwordresetReq = new ChangePasswordDto()
                {
                    Username = request.Username,
                    OldPassword = request.OldPassword,
                    NewPassword = request.NewPassword,
                    ConfirmPassword = request.ConfirmPassword
                };
                var resp = await _authRepo.ChangePassword(passwordresetReq);
                return resp;

            }
            catch (Exception ex)
            {
                this.logger.LogError(exception: ex, ex.Message);
                return null;
            }
        }
    }
}
