using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer
{
    [Table("DropdownMaster")]
    public class DropdownMaster:SharedTableModel
    {
        [Key]
        public int DropdownDataId { get; set; }
        public string DataType { get; set; }
        public string DataText { get; set; }
        public string DataValue { get; set; }

    }
}
