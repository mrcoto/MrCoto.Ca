using System.Collections.Generic;
using System.Linq;

namespace MrCoto.Ca.Application.Common.Mail.Data
{
    public class MailTemplateData
    {
        public string Subject { get; set; }
        public List<string> Emails { get; set; }

        public MailTemplateData()
        {
            
        }

        public MailTemplateData(string email, string subject)
        {
            Emails = new List<string> {email};
            Subject = subject;
        }

        public MailTemplateData(List<string> emails, string subject)
        {
            Emails = emails.Distinct().ToList();
            Subject = subject;
        }
    }
}