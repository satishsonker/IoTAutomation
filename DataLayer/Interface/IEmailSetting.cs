using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
   public interface IEmailSetting
    {
        Task<EmailSetting> GetEmailSetting(string userKey);
        Task<int> UpdateEmailSetting(EmailSetting emailSetting, string userKey);
        Task<int> DeleteEmailSetting(int settingId, string userKey);
    }
}
