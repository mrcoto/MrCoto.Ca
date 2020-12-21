namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.NewLogin
{
    public class NewLoginMailData
    {
        public string Username { get; set; }
        public string Device { get; set; }
        public string DeviceType { get; set; }
        public string Browser { get; set; }
        public string Ip { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}