using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Application.Common.CurrentUsers.Response
{
    public class CurrentTenant
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}