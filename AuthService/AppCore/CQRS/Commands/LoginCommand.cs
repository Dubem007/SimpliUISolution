﻿using AuthService.Domain.Dtos;
using MediatR;
using OnaxTools.Dto.Http;

namespace AuthService.AppCore.CQRS.Commands
{
    public class LoginCommand : IRequest<GenResponse<UserLoginResponse>>
    {
        public LoginCommand(string userName, string password)
        {

            this.UserName = userName;
            this.password = password;
        }

        public string UserName { get; private set; }
        public string password { get; private set; }
    }
}
