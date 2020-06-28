﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Reliance.Web.ThisApp.Infrastructure;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Reliance.Web.ThisApp.Services.Support
{
    public class EmailSender : IEmailSender
    {
        public bool IsHtmlBody { get; set; } = true;
        public bool EnableSsl { get; set; } = false;

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            IsHtmlBody = true;
            Send(email, subject, htmlMessage);
            return Task.CompletedTask;
        }

        private SmtpClient _emailClient;
        public SmtpClient EmailClient
        {
            get
            {
                if (_emailClient == null)
                {
                    _emailClient = new SmtpClient(PrivateSettings.EmailServerAddress)
                    {
                        UseDefaultCredentials = false,
                        Port = PrivateSettings.EmailServerPort,
                        EnableSsl = PrivateSettings.EmailEnableSsl,
                        Credentials = new NetworkCredential(PrivateSettings.EmailUserName, PrivateSettings.EmailPassword) // TODO: better way?
                    };
                }
                return _emailClient;
            }
        }

        public void Send(string sendTo, string subject, string messageBody)
        {
            var mailMessage = Message(subject, messageBody);
            mailMessage.To.Add(sendTo);
            EmailClient.Send(mailMessage);
        }

        public void Send(string[] sendTo, string subject, string messageBody)
        {
            var mailMessage = Message(subject, messageBody);
            foreach (var adr in sendTo)
            {
                if (!string.IsNullOrWhiteSpace(adr))
                    mailMessage.To.Add(adr);
            }
            EmailClient.Send(mailMessage);
        }

        private MailMessage Message(string subject, string messageBody)
        {
            return new MailMessage
            {
                From = new MailAddress(PrivateSettings.EmailUserName), // TODO: Mov to Azure Vault
                Subject = subject,
                IsBodyHtml = IsHtmlBody,
                Body = messageBody
            };
        }
    }
}
