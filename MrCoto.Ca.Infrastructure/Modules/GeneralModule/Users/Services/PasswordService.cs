using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Services
{
    public class PasswordService : IPasswordService
    {
        public string Hash(string rawPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(rawPassword);
        }

        public bool Verify(string rawPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(rawPassword, password);
        }
    }
}