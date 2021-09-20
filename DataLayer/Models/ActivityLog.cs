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
        [Required(AllowEmptyStrings =false,ErrorMessage ="UserKey is required")]
        public string UserKey { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "AppName is required")]
        public string AppName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Activity is required")]
        public string Activity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
