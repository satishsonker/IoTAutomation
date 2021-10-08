using System;
using System.Collections.Generic;
using System.Text;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;

namespace IoT.BusinessLayer
{
   public class RoomBL
    {
        private readonly IRooms _rooms;
        public RoomBL(IRooms room)
        {
            _rooms = room;
        }

        public Room AddRoom(Room room, string userKey)
        {
            if (room != null)
            {
                room.CreatedDate = DateTime.Now;
                room.RoomKey = Guid.NewGuid().ToString().Replace("-","").ToUpper();
            }
            _rooms.Add(room,userKey);
            return room;
        }
        public Room GetRoom(string roomKey, string userKey)
        {
            return _rooms.GetRoom(roomKey, userKey);
        }
        public IEnumerable<Room> GetAllRoom(string userKey)
        {
          return  _rooms.GetAllRooms(userKey);
        }

        public IEnumerable<object> GetRoomDropdown(string userKey)
        {
            return _rooms.GetRoomDropdown(userKey);
        }
        public IEnumerable<Room> SearchRoom(string searchTerm, string userKey)
        {
            return _rooms.SearchRooms(searchTerm, userKey);
        }
        public Room DeleteRoom(string roomKey, string userKey)
        {
            return _rooms.Delete(roomKey,userKey);
        }
        public Room UpdateRoom(Room room, string userKey)
        {
            return _rooms.Update(room, userKey);
        }
    }
}
