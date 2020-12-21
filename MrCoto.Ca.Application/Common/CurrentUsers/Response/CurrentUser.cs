using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Application.Common.CurrentUsers.Response
{
    public class CurrentUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CurrentRole Role { get; set; }
        public CurrentTenant Tenant { get; set; }
    }
}