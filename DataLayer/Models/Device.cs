using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("Devices")]
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int DeviceTypeId { get; set; }
        public string DeviceDesc { get; set; }
        public string DeviceKey { get; set; }
        public int RoomId { get; set; }
        public string UserKey { get; set; }
        public DateTime? LastConnected { get; set; }
        public int ConnectionCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DeviceType DeviceType { get; set; }
        public Room Room { get; set; }
    }
}
