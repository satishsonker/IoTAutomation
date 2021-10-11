using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer
{
    [Table("DeviceType")]
    public class DeviceType:SharedTableModel
    {
        [Key]
        public int DeviceTypeId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Device Type Name is required")]
        public string DeviceTypeName { get; set; }
        public ICollection<DeviceAction> DeviceActions { get; set; }
        public ICollection<DeviceCapability> DeviceCapabilities { get; set; }
        public string UserKey { get; set; }
    }
}
