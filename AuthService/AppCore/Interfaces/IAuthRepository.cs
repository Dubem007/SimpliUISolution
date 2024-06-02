using AuthService.Domain.Dtos;
using OnaxTools.Dto.Http;

namespace AuthService.AppCore.Interfaces
{
    public interface IAuthRepository
    {
        Task<GenResponse<UserCreationResponseDto>> CreateUser(UserCreationRequestDto input);
        Task<GenResponse<UserLoginResponse>> Login(UserLoginDto model);
        Task<GenResponse<string>> UnlockAccount(UnclockAccountDto model);
        Task<GenResponse<string>> ChangePassword(ChangePasswordDto model);
    }
}
