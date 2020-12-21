using System.Collections.Generic;

namespace MrCoto.Ca.Application.Common.Mail.Data
{
    public class MailData
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Emails { get; set; }
        public bool IsHtml { get; set; } = false;
    }
}