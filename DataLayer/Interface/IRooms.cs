using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.ModelLayer;

namespace IoT.DataLayer.Interface
{
  public  interface IRooms
    {
        Task<Room> Add(Room newRoom, string userKey);
        Task<Room> Update(Room updateRoom,string userKey);
        Task<Room> Delete(string roomKey ,string userKey);
        Task<PagingRecord> GetAllRooms(string userKey,int pageNo, int pageSize);
        Task<Room> GetRoom(string roomKey, string userKey);
        Task<List<Room>> SearchRooms(string searchTerm, string userKey);
        Task<List<object>> GetRoomDropdown(string userKey);
    }
}
