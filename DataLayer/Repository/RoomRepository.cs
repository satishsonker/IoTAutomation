using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Repository
{
    public class RoomRepository : IRooms
    {
        private readonly AppDbContext context;
        public RoomRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Room> Add(Room newRoom, string userKey)
        {
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var room =await context.Rooms.Where(x => x.RoomId == newRoom.RoomId && x.UserKey == userKey).FirstOrDefaultAsync();
                if (room == null)
                {
                    newRoom.CreatedDate = DateTime.Now;
                    context.Rooms.Add(newRoom);
                   await context.SaveChangesAsync();
                }
            }
            return newRoom;
        }

        public async Task<Room> Delete(string roomKey, string userKey)
        {
            var deleteRoom =await context.Rooms.Where(x => x.RoomKey == roomKey && x.UserKey == userKey).FirstOrDefaultAsync();
            if (deleteRoom != null)
            {
                var room = context.Rooms.Attach(deleteRoom);
                room.State = EntityState.Deleted;
               await context.SaveChangesAsync();
            }
            return deleteRoom;
        }

        public async Task<List<Room>> GetAllRooms(string userKey)
        {
            return await context.Rooms
                .Where(x => x.UserKey == userKey)
                .OrderBy(x => x.RoomName)
                .ToListAsync();
        }

        public async Task<Room> GetRoom(string roomKey, string userKey)
        {
            return await context.Rooms
                .Where(x => x.RoomKey == roomKey && x.UserKey == userKey)
                .FirstOrDefaultAsync();
        }

        public async Task<List<dynamic>> GetRoomDropdown(string userKey)
        {
            return await Task.Factory.StartNew(() =>
             context.Rooms
                .Where(x => x.UserKey == userKey)
                .Select(x => new { x.RoomId, x.RoomKey, x.RoomName })
                .OrderBy(x => x.RoomName).AsEnumerable().Cast<dynamic>()
                .ToList()
            );
        }

        public async Task<List<Room>> SearchRooms(string searchTerm, string userKey)
        {
            return await context.Rooms
                .Where(x => x.UserKey == userKey && (searchTerm == "All" || x.RoomName.Contains(searchTerm) || x.RoomKey.Contains(searchTerm) || x.RoomDesc.Contains(searchTerm)))
                .OrderBy(x => x.RoomName)
                .ToListAsync();
        }

        public async Task<Room> Update(Room updateRoom, string userKey)
        {
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var room = context.Rooms.Attach(updateRoom);
                room.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return updateRoom;
        }
    }
}
