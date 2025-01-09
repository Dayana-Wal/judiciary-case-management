using CaseManagement.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace CaseManagement.Business.Service
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {  
            _emailSettings = emailSettings.Value;
        }
        //TODO
        //public async Task SendEmail(string toEmail, string subject, string body)
        //{
        //    try
        //    {
        //        using var smtpClient = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
        //        {
        //            Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
        //            EnableSsl = true
        //        };

        //        var mailMessage = new MailMessage
        //        {
        //            From = new MailAddress(_emailSettings.SenderEmail),
        //            Subject = subject,
        //            Body = body,
        //            IsBodyHtml = true
        //        };

        //        mailMessage.To.Add(new MailAddress(toEmail));

        //        await smtpClient.SendMailAsync(mailMessage);
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}


        public async Task SendEmail(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sender Name", _emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            email.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync(_emailSettings.SmtpHost, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to send email: {ex.Message}");
                throw;
            }
        }

    }
}
