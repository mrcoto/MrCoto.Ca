using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using MrCoto.Ca.Application.Common.ClientDetection;
using MrCoto.Ca.Application.Common.Events;
using MrCoto.Ca.Application.Common.Mail;
using MrCoto.Ca.Application.Common.Mail.Data;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.NewLogin;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Listeners
{
    public class UserLoggedRegisterDeviceListener : INotificationHandler<DomainEventNotification<UserLogged>>
    {
        private readonly IUowGeneral _uowGeneral;
        private readonly IClientInfoDetection _clientInfoDetection;
        private readonly IMailService _mailService;
        private readonly ILogger<UserLoggedRegisterDeviceListener> _logger;
        
        private const double Tolerance = Double.Epsilon;

        public UserLoggedRegisterDeviceListener(
            IUowGeneral uowGeneral,
            IClientInfoDetection clientInfoDetection,
            IMailService mailService,
            ILogger<UserLoggedRegisterDeviceListener> logger)
        {
            _uowGeneral = uowGeneral;
            _clientInfoDetection = clientInfoDetection;
            _mailService = mailService;
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<UserLogged> notification, CancellationToken cancellationToken)
        {
            var user = notification.DomainEvent.User;
            _logger.LogInformation($"Registring Device for User: {user.Name}");

            var lastUserLogin = await _uowGeneral.UserLoginRepository.LastUserLogin(user.Id);
            
            var userLogin = await GenerateUserLogin(user);
            await _uowGeneral.UserLoginRepository.Create(userLogin);
           
            await _uowGeneral.SaveChanges();

            if (lastUserLogin == null || !AreEquals(userLogin, lastUserLogin))
            {
                await SendNewLoginMail(user, userLogin);
            }
        }

        private async Task<UserLogin> GenerateUserLogin(User user)
        {
            var ip = await _clientInfoDetection.Ip();
            var location = await _clientInfoDetection.Location(ip);
            var deviceInfo = await _clientInfoDetection.DeviceInfo();

            return new UserLogin()
            {
                UserId = user.Id,
                User = user,
                Device = deviceInfo.Device,
                DeviceType = deviceInfo.DeviceType,
                DeviceFamily = deviceInfo.DeviceFamily,
                Browser = deviceInfo.Browser,
                BrowserFamily = deviceInfo.BrowserFamily,
                UserAgent = deviceInfo.UserAgent,
                ClientIp = IPAddress.Parse(ip),
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Country = location.CountryName,
                Region = location.RegionName,
                City = location.CityName,
                DeletedAt = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
        }

        private bool AreEquals(UserLogin orginal, UserLogin other)
        {
            return
                orginal.UserId == other.UserId &&
                orginal.Device == other.Device &&
                orginal.DeviceFamily == other.DeviceFamily &&
                orginal.DeviceType == other.DeviceType &&
                orginal.Browser == other.Browser &&
                orginal.BrowserFamily == other.BrowserFamily &&
                orginal.UserAgent == other.UserAgent &&
                Equals(orginal.ClientIp, other.ClientIp) &&
                Math.Abs(orginal.Latitude - other.Latitude) < Tolerance &&
                Math.Abs(orginal.Longitude - other.Longitude) < Tolerance &&
                orginal.Country == other.Country &&
                orginal.Region == other.Region &&
                orginal.City == other.City;
        }

        private async Task SendNewLoginMail(User user, UserLogin userLogin)
        {
            var newLoginData = new NewLoginMailData()
            {
                Username = user.Name,
                Device = userLogin.Device,
                DeviceType = userLogin.DeviceType,
                Browser = userLogin.Browser,
                Ip = userLogin.ClientIp.MapToIPv4().ToString(),
                Country = userLogin.Country,
                Region = userLogin.Region,
                City = userLogin.City,
                Latitude = userLogin.Latitude,
                Longitude = userLogin.Longitude,
            };
            var templateData = new MailTemplateData(user.Email, "Se ha detectado un nuevo inicio de sesión");
            await _mailService.Enqueue(templateData, typeof(INewLoginMail), newLoginData);
        }
    }
}