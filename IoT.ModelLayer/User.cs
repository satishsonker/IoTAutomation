using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer
{
    [Table("Users")]
    public class User:SharedTableModel
    {
        [Key]
        public int UserId { get; set; }
        public string AuthProvidor { get; set; }
        [EmailAddress(ErrorMessage ="Invalid email id")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Firstname is required")]
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string APIKey { get; set; }
        public string Language { get; set; }
        public string Timezone { get; set; }
        public ICollection<UserPermission> UserPermissions  { get; set; }
        public string Temperature { get; set; }
        public DateTime LastLogin { get; set; }

    }
}
