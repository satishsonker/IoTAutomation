using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.DataLayer.Models
{
    [Table("Scenes")]
    public class Scene
    {
        [Key]
        public int SceneId { get; set; }
        public string SceneKey { get; set; }
        public string SceneName { get; set; }
        public string? SceneDesc { get; set; }
        public string UserKey { get; set; }
        public Device Device { get; set; }
        public ICollection<SceneAction> SceneActions { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
