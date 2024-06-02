using AuthService.AppCore.CQRS.Commands;
using AuthService.AppCore.Interfaces;
using AuthService.Domain.Dtos;
using MediatR;
using OnaxTools.Dto.Http;

namespace AuthService.AppCore.CQRS.Handlers.CommandHandlers
{
    public class UnlockAccountCommandHandler : IRequestHandler<UnlockAccountCommand, GenResponse<string>>
    {
        private readonly ILogger<UnlockAccountCommand> logger;
        private readonly IAuthRepository _authRepo;
        private readonly IMediator mediator;

        public UnlockAccountCommandHandler(ILogger<UnlockAccountCommand> logger, IAuthRepository authRepo, IMediator mediator)
        {
            this.logger = logger;
            _authRepo = authRepo;
            this.mediator = mediator;
        }
        public async Task<GenResponse<string>> Handle(UnlockAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var unlockaccountReq = new UnclockAccountDto()
                {
                    Username = request.Username,
                    ResetCode = request.ResetCode,
                };
                var resp = await _authRepo.UnlockAccount(unlockaccountReq);
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
