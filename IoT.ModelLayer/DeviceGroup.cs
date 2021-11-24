using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    [Table("DeviceDroup")]
   public class DeviceGroup
    {
        [Key]
        [JsonIgnore]
        public int GroupId { get; set; }
        public string GroupKey { get; set; }
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
