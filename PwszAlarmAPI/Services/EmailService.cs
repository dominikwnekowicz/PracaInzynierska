using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace PwszAlarmAPI.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }
        private async Task configSendGridasync(IdentityMessage message)
        {
            Environment.SetEnvironmentVariable("apiKey", "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");
            var apiKey = Environment.GetEnvironmentVariable("apiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@pwszalarm.net", "PWSZ Alarm");
            var subject = message.Subject;
            var to = new EmailAddress(message.Destination, message.Destination.Split('@').ElementAt(1));
            var plainTextContent = message.Body;
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if(response.StatusCode != HttpStatusCode.OK)
            {
                await Task.FromResult(0);
            }
        }
    }

 }