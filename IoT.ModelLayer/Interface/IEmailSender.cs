using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.ModelLayer.Interface
{
   public interface IEmailSender
    {
        Task<bool> SendEmailAsync(MimeMessage mimeMessage);
    }
}
