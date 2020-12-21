using System;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using MrCoto.Ca.Application.Common.Background;
using MrCoto.Ca.Application.Common.Mail;
using MrCoto.Ca.Application.Common.Mail.Data;
using MrCoto.Ca.Application.Common.Renderers;

namespace MrCoto.Ca.Infrastructure.Common.Mail
{
    public class MailService : IMailService
    {
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly IFluentEmail _email;
        private readonly IViewRenderer _viewRenderer;
        private readonly IServiceProvider _serviceProvider;

        public MailService(
            IBackgroundJobService backgroundJobService, 
            IFluentEmail email, 
            IViewRenderer viewRenderer,
            IServiceProvider serviceProvider
            )
        {
            _backgroundJobService = backgroundJobService;
            _email = email;
            _viewRenderer = viewRenderer;
            _serviceProvider = serviceProvider;
        }
        
        public async Task Send<TData>(MailTemplateData mailData, Type template, TData data)
        {
            var mailTemplate = _serviceProvider.GetRequiredService(template);
            if (!(mailTemplate is IMailTemplate<TData>))
            {
                throw new ArgumentException($"{template.FullName} debe implementar la interfaz {typeof(IMailTemplate<>).FullName}");
            }
            (mailTemplate as IMailTemplate<TData>).Data = data;
            
            var templateName = mailTemplate.GetType().FullName ?? "";
            templateName = templateName.Substring(0, templateName.Length - 5);

            var body = await _viewRenderer.Render(templateName, mailTemplate);
            
            await _email
                .To(mailData.Emails.Select(email => new Address(email)).ToList())
                .Subject(mailData.Subject)
                .Body(body, isHtml: true)
                .SendAsync();
        }
        
        public async Task<string> Enqueue<TData>(MailTemplateData mailData, Type template, TData data)
        {
            try
            {
                return await _backgroundJobService.Enqueue(() => Send(mailData, template, data));
            }
            catch (Exception) // Fallback
            {
                await Send(mailData, template, data);
            }

            return "";
        }

        public async Task Send(MailData mailData)
        {
            await _email
                .To(mailData.Emails.Select(email => new Address(email)).ToList())
                .Subject(mailData.Subject)
                .Body(mailData.Body, mailData.IsHtml)
                .SendAsync();
        }

        public async Task<string> Enqueue(MailData mailData)
        {
            return await _backgroundJobService.Enqueue(() => Send(mailData) );
        }
    }
}