using System;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.Mail.Data;

namespace MrCoto.Ca.Application.Common.Mail
{
    public interface IMailService
    {
        public Task Send<TData>(MailTemplateData mailData, Type template, TData data);
        public Task<string> Enqueue<TData>(MailTemplateData mailData, Type template, TData data);
        public Task Send(MailData mailData);
        public Task<string> Enqueue(MailData mailData);
    }
}