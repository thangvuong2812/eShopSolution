using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Services
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }
        private const string emailServer = "thangvuong2812@gmail.com";
        public async Task<bool> SendMailTwoFactor(string email, string code)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(emailServer);
            mailMessage.To.Add(new MailAddress(email));

            mailMessage.Subject = "Two Factor Code";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = code;

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Credentials = new NetworkCredential(emailServer, "xrsjspxcpljxhpql");
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                
            }
            return false;
        }
    }
}
