using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    [Table("Devices")]
    [DataContract]
    public class Device:SharedTableModel
    {
        [Key]
        [Range(minimum:0,maximum:int.MaxValue,ErrorMessage ="DeviceId is required")]
        public int DeviceId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Device name is required")]
        public string DeviceName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Device friendly name is required")]
        public string FriendlyName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Device friendly name is required")]
        public string ManufacturerName { get; set; }
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "DeviceTypeId is required")]
        public int DeviceTypeId { get; set; }
        public string DeviceDesc { get; set; }
        public string DeviceKey { get; set; }
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "RoomId is required")]
        public int RoomId { get; set; }
        public DateTime? LastConnected { get; set; }
        public int ConnectionCount { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }
    }
}
