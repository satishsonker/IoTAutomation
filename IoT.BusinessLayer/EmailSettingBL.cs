using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
   public class EmailSettingBL
    {
        private readonly IEmailSetting setting;
        public EmailSettingBL(IEmailSetting emailSetting)
        {
            setting = emailSetting;
        }

        public async Task<int> DeleteEmailSetting(int settingId, string userKey)
        {
            return await setting.DeleteEmailSetting(settingId, userKey);
        }

        public async Task<EmailSetting> GetEmailSetting(string userKey)
        {
            return await setting.GetEmailSetting(userKey);
        }

        public async Task<int> UpdateEmailSetting(EmailSetting emailSetting, string userKey)
        {
            emailSetting.UserKey = userKey;
            emailSetting.CreatedDate = DateTime.Now;
            emailSetting.ModifiedDate = DateTime.Now;
            return await setting.UpdateEmailSetting(emailSetting, userKey);
        }
    }
}
