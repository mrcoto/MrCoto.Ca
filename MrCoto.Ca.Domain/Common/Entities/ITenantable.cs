namespace MrCoto.Ca.Domain.Common.Entities
{
    public interface ITenantable 
    {
        public long TenantId { get; set; }
    }
}