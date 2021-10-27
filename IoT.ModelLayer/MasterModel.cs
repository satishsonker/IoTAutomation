using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    [Table("CapabilityTypes")]
   public class CapabilityType:SharedTableModelNoUserKey
    {
        [Key]
        [Range(minimum:0,maximum:int.MaxValue,ErrorMessage = "CapabilityTypeId is required")]
        public int CapabilityTypeId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage = "CapabilityTypeName is required")]
        public string CapabilityTypeName { get; set; }
    }

    [Table("CapabilityVersion")]
    public class CapabilityVersion : SharedTableModelNoUserKey
    {
        [Key]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "CapabilityVersionId is required")]
        public int CapabilityVersionId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CapabilityVersionName is required")]
        public string CapabilityVersionName { get; set; }
    }
    [Table("DisplayCategory")]
    public class DisplayCategory : SharedTableModelNoUserKey
    {
        [Key]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "DisplayCategoryId is required")]
        public int DisplayCategoryId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayCategoryValue is required")]
        public string DisplayCategoryValue { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "DisplayCategoryLabel is required")]
        public string DisplayCategoryLabel { get; set; }
    }
    [Table("CapabilityInterface")]
    public class CapabilityInterface : SharedTableModelNoUserKey
    {
        [Key]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "CapabilityInterfaceId is required")]
        public int CapabilityInterfaceId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CapabilityInterfaceName is required")]
        public string CapabilityInterfaceName { get; set; }
    }
    [Table("CapabilitySupportedProperty")]
    public class CapabilitySupportedProperty : SharedTableModelNoUserKey
    {
        [Key]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "CapabilitySupportedPropertyId is required")]
        public int CapabilitySupportedPropertyId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CapabilitySupportedPropertyName is required")]
        public string CapabilitySupportedPropertyName { get; set; }
    }

    public class AllCapabilityMasterModel
    {
        public IEnumerable<DropdownDataModel> CapabilitySupportedProperties { get; set; }
        public IEnumerable<DropdownDataModel> CapabilityInterfaces { get; set; }
        public IEnumerable<DropdownDataModel> DisplayCategories { get; set; }
        public IEnumerable<DropdownDataModel> CapabilityVersions { get; set; }
        public IEnumerable<DropdownDataModel> CapabilityTypes { get; set; }
    }

    public class DropdownDataModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
