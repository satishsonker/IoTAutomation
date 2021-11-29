using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    [Table("DeviceGroupDetails")]
    public class DeviceGroupDetail
    {
        [Key]
        [JsonIgnore]
        public int GroupDetailId { get; set; }
        [JsonIgnore]
        public int GroupId { get; set; }
       
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        [JsonIgnore]
        public DeviceGroup DeviceGroup { get; set; }
        [JsonIgnore]
        public string UserKey { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
    }
}
