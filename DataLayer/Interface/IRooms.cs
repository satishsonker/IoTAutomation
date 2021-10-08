using System;
using System.Collections.Generic;
using System.Text;
using IoT.ModelLayer;

namespace IoT.DataLayer.Interface
{
  public  interface IRooms
    {
        Room Add(Room newRoom, string userKey);
        Room Update(Room updateRoom,string userKey);
        Room Delete(string roomKey ,string userKey);
        IEnumerable<Room> GetAllRooms(string userKey);
        Room GetRoom(string roomKey, string userKey);
        IEnumerable<Room> SearchRooms(string searchTerm, string userKey);
        IEnumerable<object> GetRoomDropdown(string userKey);
    }
}
