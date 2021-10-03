using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("CapabilityTypes")]
   public class CapabilityType
    {
        [Key]
        public int CapabilityTypeId { get; set; }
        public string CapabilityTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    [Table("CapabilityVersion")]
    public class CapabilityVersion
    {
        [Key]
        public int CapabilityVersionId { get; set; }
        public string CapabilityVersionName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    [Table("DisplayCategory")]
    public class DisplayCategory
    {
        [Key]
        public int DisplayCategoryId { get; set; }
        public string DisplayCategoryValue { get; set; }
        public string DisplayCategoryLabel { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    [Table("CapabilityInterface")]
    public class CapabilityInterface
    {
        [Key]
        public int CapabilityInterfaceId { get; set; }
        public string CapabilityInterfaceName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    [Table("CapabilitySupportedProperty")]
    public class CapabilitySupportedProperty
    {
        [Key]
        public int CapabilitySupportedPropertyId { get; set; }
        public string CapabilitySupportedPropertyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
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
