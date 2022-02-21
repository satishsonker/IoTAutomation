using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    [Table("UserPermission")]
    public class UserPermission:SharedTableModel
    {
        [Key]
        public int UserPermissionId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="User key is required")]
        new public string UserKey { get; set; }
        [Range(minimum:1,maximum:int.MaxValue, ErrorMessage ="Invalid user Id")]
        public int UserId { get; set; }
        public bool CanView { get; set; }
        public User User { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanCreate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
