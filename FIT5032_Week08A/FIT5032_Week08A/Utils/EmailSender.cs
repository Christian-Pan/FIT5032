using Microsoft.ApplicationInsights.Extensibility.Implementation;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const string API_KEY = "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";


        public void Send(string toEmail, string subject, string contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("christianpandapan@gmail.com", "Algorithimic Thinking");
            var to = new EmailAddress(toEmail, "Learner");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            byte[] byteData = Encoding.ASCII.GetBytes(File.ReadAllText(@"~\Attachments\sample_attachment.txt"));
            msg.Attachments.Add(new Attachment
            {
                Content = Convert.ToBase64String(byteData),
                Filename = "attachment.txt",
                Type = "txt/plain",
                Disposition = "attachment"
            });

            var response = client.SendEmailAsync(msg);
        }

    }
}