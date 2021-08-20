using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("DeviceType")]
    public class DeviceType
    {
        [Key]
        public int DeviceTypeId { get; set; }
        public string DeviceTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
