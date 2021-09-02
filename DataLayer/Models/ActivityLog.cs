using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("ActivityLog")]
    public class ActivityLog
    {
        [Key]
        public int ActivityLogId { get; set; }
        public string UserKey { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }
        public string AppName { get; set; }
        public string Activity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
