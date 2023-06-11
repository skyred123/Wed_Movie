using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace Wed_Movie.SendMail
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage myMail = new MailMessage();
            myMail.From = new MailAddress(_emailConfig.From);
            myMail.To.Add(email);
            myMail.Subject = subject;
            myMail.IsBodyHtml = true;
            myMail.Body = htmlMessage;

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(myMail.From.ToString(), _emailConfig.Password);
                client.EnableSsl = true;
                client.Send(myMail);
            }

            return Task.CompletedTask;
        }
    }
}
