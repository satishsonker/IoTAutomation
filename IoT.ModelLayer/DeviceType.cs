using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    [Table("DeviceType")]
    public class DeviceType:SharedTableModelNoUserKey
    {
        [Key]
        public int DeviceTypeId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Device Type Name is required")]
        public string DeviceTypeName { get; set; }
        public bool IsAlexaCompatible { get; set; }
        public bool IsGoogleCompatible { get; set; }
        [JsonIgnore]
        public ICollection<DeviceAction> DeviceActions { get; set; }
        public ICollection<DeviceCapability> DeviceCapabilities { get; set; }
    }
}
