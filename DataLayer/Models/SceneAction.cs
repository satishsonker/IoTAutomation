using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("SceneActions")]
    public class SceneAction
    {
        [Key]
        public int SceneActionId { get; set; }
        public string SceneActionKey { get; set; }
        public int SceneId { get; set; }
        public int DeviceId { get; set; }
        public string Action{ get; set; }
        public string? Value { get; set; }
        public string UserKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public Scene Scene { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
