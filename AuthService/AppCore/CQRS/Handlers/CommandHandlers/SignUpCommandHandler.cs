using AuthService.AppCore.CQRS.Commands;
using AuthService.AppCore.Interfaces;
using AuthService.Domain.Dtos;
using MediatR;
using OnaxTools.Dto.Http;
using OnaxTools.Http;

namespace AuthService.AppCore.CQRS.Handlers.CommandHandlers
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, GenResponse<UserCreationResponseDto>>
    {
        #region Variable Declarations
        private readonly ILogger<SignUpCommandHandler> logger;
        private readonly IAuthRepository _authRepo;
        private readonly IMediator mediator;

        public SignUpCommandHandler(ILogger<SignUpCommandHandler> logger, IAuthRepository authRepo, IMediator mediator)
        {
            this.logger = logger;
            _authRepo = authRepo;
            this.mediator = mediator;
        }
        #endregion
        public async Task<GenResponse<UserCreationResponseDto>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
			try
			{
                var resp = await _authRepo.CreateUser(request.Entity);
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
