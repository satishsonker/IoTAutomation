using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Room> AddRoom(Room room, string userKey)
        {
            if (room != null)
            {
                room.CreatedDate = DateTime.Now;
                room.RoomKey = Guid.NewGuid().ToString().Replace("-","").ToUpper();
            }
            await _rooms.Add(room,userKey);
            return room;
        }
        public async Task<Room> GetRoom(string roomKey, string userKey)
        {
            return await _rooms.GetRoom(roomKey, userKey);
        }
        public async Task<PagingRecord> GetAllRoom(string userKey,int pageNo, int pageSize)
        {
          return await  _rooms.GetAllRooms(userKey,pageNo,pageSize);
        }

        public async Task<List<object>> GetRoomDropdown(string userKey)
        {
            return await _rooms.GetRoomDropdown(userKey);
        }
        public async Task<List<Room>> SearchRoom(string searchTerm, string userKey)
        {
            return await _rooms.SearchRooms(searchTerm, userKey);
        }
        public async Task<Room> DeleteRoom(string roomKey, string userKey)
        {
            return await _rooms.Delete(roomKey,userKey);
        }
        public async Task<Room> UpdateRoom(Room room, string userKey)
        {
            return await _rooms.Update(room, userKey);
        }
    }
}
