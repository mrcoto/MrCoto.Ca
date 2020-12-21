using System;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.Mail;
using MrCoto.Ca.Application.Common.Mail.Data;

namespace MrCoto.Ca.WebApiTests.Fake
{
    public class FakeMailService : IMailService
    {
        public Task Send<TData>(MailTemplateData mailData, Type template, TData data)
        {
            return Task.CompletedTask;
        }

        public Task<string> Enqueue<TData>(MailTemplateData mailData, Type template, TData data)
        {
            return Task.FromResult("");
        }

        public Task Send(MailData mailData)
        {
            return Task.CompletedTask;
        }

        public Task<string> Enqueue(MailData mailData)
        {
            return Task.FromResult("");
        }
    }
}