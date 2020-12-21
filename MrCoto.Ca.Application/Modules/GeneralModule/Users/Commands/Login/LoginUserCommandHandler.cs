using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login.Response;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUowGeneral _uowGeneral;
        private readonly IPasswordService _passwordService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly RefreshTokenService _refreshTokenService;

        public LoginUserCommandHandler(
            IUowGeneral uowGeneral, 
            IPasswordService passwordService, 
            IAccessTokenService accessTokenService,
            RefreshTokenService refreshTokenService)
        {
            _uowGeneral = uowGeneral;
            _passwordService = passwordService;
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }
        
        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _uowGeneral.UserRepository.FindByEmail(request.Email) 
                       ?? throw new InvalidAccountException();

            if (user.HasDisabledAccount()) throw new InvalidAccountException("Cuenta deshabilitada");
            
            var loginMaxAttempt = await _uowGeneral.LoginMaxAttemptRepository.Find(GeneralConstants.DefaultId);
            if (await HasReachedLoginMaxAttempts(user, loginMaxAttempt.MaxAttempts))
            {
                throw new LoginMaxAttemptsReachedException();
            }

            if (!_passwordService.Verify(request.Password, user.Password))
            {
                await HandleFailedLogin(user, loginMaxAttempt.MaxAttempts);
                throw new InvalidAccountException();
            }
        
            user.AddEvent(new UserLogged() { User = user });
            await AllowUserAccess(user);
            
            var accessToken = await _accessTokenService.GetAccessToken(user);
            var refreshToken = await _refreshTokenService.GetRefreshToken(user);
            
            await _uowGeneral.SaveChanges();
        
            return new LoginUserResponse() { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        private async Task HandleFailedLogin(User user, int maxAttempts)
        {
            user.LoginAttempts += 1;
            if (user.LoginAttempts >= maxAttempts)
            {
                user.AddEvent(new MaxLoginAttemptsReached() { User = user});
            }
            await _uowGeneral.SaveChanges();
        }

        private async Task AllowUserAccess(User user)
        {
            user.LoginAttempts = 0;
            user.LastLoginAt = DateTime.Now;
            await _uowGeneral.UserRepository.Update(user);
        }

        private Task<bool> HasReachedLoginMaxAttempts(User user, int maxAttempts)
        {
            return Task.FromResult(user.LoginAttempts >= maxAttempts);
        }
        
    }
}