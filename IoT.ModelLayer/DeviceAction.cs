using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer
{
    [Table("DeviceActions")]
    public class DeviceAction:SharedTableModel
    {
        [Key]
        [Column(Order = 1)]
        public int DeciveActionId { get; set; }

        [Column(Order = 2)]
        public int DeviceTypeId { get; set; }

        [Column("DeciveActionName", Order = 3)]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Device action name is required")]
        public string DeviceActionName { get; set; }

        [Column(Order = 4)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Device Action Name Backend is required")]
        public string DeviceActionNameBackEnd { get; set; }

        [Column(Order = 5)]
        public string DeviceActionValue { get; set; }

        [Column(Order = 6)]
        public DeviceType DeviceType { get; set; }
    }
}
