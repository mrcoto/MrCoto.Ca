namespace MrCoto.Ca.Domain.Modules.GeneralModule.Constants
{
    public class GeneralConstants
    {
        public const long DefaultId = 1L;
        
        public const string RoleSuperAdmin = "super_admin";
        public const string RoleUser = "user";
        
        public const int DisablementBlockedByUserId = 1;
        public const int DisablementBlockedByLoginTriesId = 2;
        public const int DisablementUnblockedId = 3;

        public const int DefaultMaxLoginAttempts = 3;

        public const string DefaultUser = "espinozasalas.jose@gmail.com";
        public const string DefaultPassword = "306@secret@306";
    }
}