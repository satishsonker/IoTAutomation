using System;
using System.Collections.Generic;
using System.Text;
using IoT.DataLayer.Models;

namespace IoT.DataLayer.Interface
{
  public  interface IRooms
    {
        Room Add(Room newRoom, string userKey);
        Room Update(Room updateRoom);
        Room Delete(string roomKey ,string userKey);
        IEnumerable<Room> GetAllRooms(string userKey);
        Room GetRoom(int roomId, string userKey);
        IEnumerable<Room> SearchRooms(string searchTerm, string userKey);
        IEnumerable<object> GetRoomDropdown(string userKey);
    }
}
