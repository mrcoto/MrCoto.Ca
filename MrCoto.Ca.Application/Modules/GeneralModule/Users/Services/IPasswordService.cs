namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Services
{
    public interface IPasswordService
    {
        public string Hash(string rawPassword);
        public bool Verify(string rawPassword, string password);
    }
}