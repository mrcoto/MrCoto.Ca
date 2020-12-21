using System;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Services.Data
{
    public class RefreshTokenData
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}