using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("DeviceCapability")]
    public class DeviceCapability
    {
        [Key]
        public int DeviceCapabilityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "DeviceType Id is required")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Invalid DeviceType")]
        public int DeviceTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Capability Type is required")]
        public string CapabilityType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Capability version is required")]
        public string Version { get; set; } = "3";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Capability Interface is required")]
        public string CapabilityInterface { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Display Category is required")]
        public string DisplayCategory { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Supported Property is required")]       
        public string SupportedProperty { get; set; }

        public bool ProactivelyReported { get; set; } = true;

        public bool Retrievable { get; set; } = true;

        public DeviceType DeviceType { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime Modifieddate { get; set; }
    }
}
