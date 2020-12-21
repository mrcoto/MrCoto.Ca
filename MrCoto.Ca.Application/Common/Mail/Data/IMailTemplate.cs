namespace MrCoto.Ca.Application.Common.Mail.Data
{
    public interface IMailTemplate<TData>
    {
        public TData Data { get; set; }
    }
}