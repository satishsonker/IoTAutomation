using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer
{
    [Table("EmailSetting")]
    public class EmailSetting : SharedTableModel
    {
        [Key]
        public int SettingId { get; set; }
        public string SMTP { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsSSL { get; set; }
    }
}
