using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    [Table("SceneActions")]
    public class SceneAction:SharedTableModel
    {
        [Key]

        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "SceneActionId is required")]
        public int SceneActionId { get; set; }
        public string SceneActionKey { get; set; }
        public Device Device { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "DeviceId is required")]
        public int DeviceId { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Action is required")]
        public string Action{ get; set; }
        public string? Value { get; set; }
        [JsonIgnore]
        public Scene Scene { get; set; }
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "SceneId is required")]
        public int SceneId { get; set; }
    }
}
