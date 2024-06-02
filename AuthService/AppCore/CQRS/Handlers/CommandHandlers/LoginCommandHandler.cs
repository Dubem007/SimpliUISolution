using AuthService.AppCore.CQRS.Commands;
using AuthService.AppCore.Interfaces;
using AuthService.Domain.Dtos;
using MediatR;
using OnaxTools.Dto.Http;

namespace AuthService.AppCore.CQRS.Handlers.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, GenResponse<UserLoginResponse>>
    {
        private readonly ILogger<LoginCommandHandler> logger;
        private readonly IAuthRepository _authRepo;
        private readonly IMediator mediator;

        public LoginCommandHandler(ILogger<LoginCommandHandler> logger, IAuthRepository authRepo, IMediator mediator)
        {
            this.logger = logger;
            _authRepo = authRepo;
            this.mediator = mediator;
        }

        public async Task<GenResponse<UserLoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loginReq = new UserLoginDto()
                {
                    Username = request.UserName,
                    Password = request.password,
                };
                var resp = await _authRepo.Login(loginReq);
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
