using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("UserPermission")]
    public class UserPermission
    {
        [Key]
        public int UserPermissionId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="User key is required")]
        public string UserKey { get; set; }
        [Range(minimum:1,maximum:int.MaxValue, ErrorMessage ="Invalid user Id")]
        public int UserId { get; set; }
        public bool CanView { get; set; }
        public User User { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanCreate { get; set; }
        public bool IsAdmin { get; set; }
        [Required(ErrorMessage ="Invalid Created Date")]
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
