using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    [Table("DeviceGroup")]
   public class DeviceGroup
    {
        [Key]
        [JsonIgnore]
        public int GroupId { get; set; }
        public string GroupKey { get; set; }
        [StringLength(maximumLength:100,MinimumLength =3,ErrorMessage ="Device group name should be min 3 chars")]
        public string GroupName { get; set; }
        [JsonIgnore]
        public string UserKey { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
        public ICollection<DeviceGroupDetail> DeviceGroupDetails { get; set; }
    }
}
