using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoT.ModelLayer
{
    [Table("Rooms")]
    public class Room:SharedTableModel
    {
        [Key]
        [Range(minimum:0,maximum:int.MaxValue,ErrorMessage ="RoomId is required")]
        public int RoomId { get; set; }
        public string RoomKey { get; set; }
        [StringLength(maximumLength:50, MinimumLength =3,ErrorMessage ="Room name required min 3 and max 50 char(s)")]
        public string RoomName { get; set; }
        public string RoomDesc { get; set; }
        public string UserKey { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
