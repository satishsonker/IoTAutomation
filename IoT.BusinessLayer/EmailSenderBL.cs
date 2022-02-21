using IoT.ModelLayer.Interface;
using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using IoT.DataLayer.Interface;

namespace IoT.BusinessLayer
{
    public class EmailSenderBL : IEmailSender
    {
        private readonly IEmailSetting emailSetting;
        public EmailSenderBL(IEmailSetting _emailSetting)
        {
            this.emailSetting = _emailSetting;
        }
        public async Task<bool> SendEmailAsync(MimeMessage message)
        {
            var client = await GetClientAsync();
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
            return true;
        }

        #region Private Methods
        private async Task<SmtpClient> GetClientAsync()
        {
            var setting = await emailSetting.GetEmailSetting(string.Empty);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect(setting.SMTP,Convert.ToInt32(setting.Port),Convert.ToBoolean(setting.IsSSL.ToString()));
            smtpClient.Authenticate(setting.Username, setting.Password);
            return smtpClient;
        }
        #endregion
    }
}
