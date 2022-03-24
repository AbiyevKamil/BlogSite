using System;
using System.Net;
using System.Net.Mail;

namespace Core.Helpers
{
    public class EmailService
    {
        private readonly string generalEmail = "stphoenix2002@gmail.com";
        private readonly string generalPass = "akamil2002";
        private readonly string companyName = "Blogar";
        
        public bool SendEmail(string url, string receiver)
        {
            try
            {
                string emailBody = $"Your email confirmation link: <a href=\"{url}\">Click here</a>";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(generalEmail);
                message.To.Add(new MailAddress(receiver));
                message.Subject = $"Email Confirmation | {companyName}";
                message.IsBodyHtml = true;
                message.Body = emailBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(generalEmail, generalPass);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}