using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer
{
    [Table("EmailTemplate")]
    public class EmailTemplate:SharedTableModel
    {
        [Key]
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Body { get; set; }
        public string Subjest { get; set; }
        public bool IsHTML { get; set; }
        public bool HasAttachment { get; set; }
        public string AttachmentPath { get; set; }
        public string Keywords { get; set; }
    }
}
