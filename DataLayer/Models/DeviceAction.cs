using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("DeviceActions")]
    public class DeviceAction
    {
        [Key]
        [Column(Order = 1)]
        public int DeciveActionId { get; set; }

        [Column(Order = 2)]
        public int DeviceTypeId { get; set; }

        [Column("DeciveActionName", Order = 3)]
        public string DeviceActionName { get; set; }

        [Column(Order = 4)]
        public string DeviceActionNameBackEnd { get; set; }

        [Column(Order = 5)]
        public string DeviceActionValue { get; set; }

        [Column(Order = 6)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 7)]
        public DateTime ModifiedDate { get; set; }

        [Column(Order = 8)]
        public DeviceType DeviceType { get; set; }
    }
}
