using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AlgorithmicThinking.Models
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("christian@algothink.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var filePath = HttpContext.Current.Server.MapPath("~/Attachments/sample_attachment.txt");
            var bytes = File.ReadAllBytes(filePath);
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("attachment.txt", file);

            var response = client.SendEmailAsync(msg);
        }

    }
}