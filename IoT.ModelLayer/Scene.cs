using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer
{
    [Table("Scenes")]
    public class Scene:SharedTableModel
    {
        [Key]
        public int SceneId { get; set; }
        public string SceneKey { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Scene name required min 3 and max 50 char(s)")]
        public string SceneName { get; set; }
        public string? SceneDesc { get; set; }
        public ICollection<SceneAction> SceneActions { get; set; }
    }
}
