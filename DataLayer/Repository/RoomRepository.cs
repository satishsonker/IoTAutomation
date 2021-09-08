using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class RoomRepository : IRooms
    {
        private readonly AppDbContext context;
        public RoomRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Room Add(Room newRoom, string userKey)
        {
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var room = context.Rooms.Where(x => x.RoomId == newRoom.RoomId && x.UserKey == userKey).FirstOrDefault();
                if (room == null)
                {
                    newRoom.CreatedDate = DateTime.Now;
                    context.Rooms.Add(newRoom);
                    context.SaveChanges();
                }
            }
            return newRoom;
        }

        public Room Delete(string roomKey, string userKey)
        {
            var deleteRoom = context.Rooms.Where(x => x.RoomKey == roomKey && x.UserKey == userKey).FirstOrDefault();
            if (deleteRoom!=null)
            {
                var room = context.Rooms.Attach(deleteRoom);
                room.State = EntityState.Deleted;
                context.SaveChanges(); 
            }
            return deleteRoom;
        }

        public IEnumerable<Room> GetAllRooms(string userKey)
        {
            return context.Rooms.Where(x=>x.UserKey==userKey).OrderBy(x=>x.RoomName);
        }

        public Room GetRoom(string roomKey, string userKey)
        {
            return context.Rooms.Where(x => x.RoomKey == roomKey && x.UserKey==userKey).FirstOrDefault();
        }

        public IEnumerable<object> GetRoomDropdown(string userKey)
        {
            return context.Rooms.Where(x=>x.UserKey==userKey).Select(x => new { x.RoomId, x.RoomKey, x.RoomName }).OrderBy(x=>x.RoomName).ToList();
        }

        public IEnumerable<Room> SearchRooms(string searchTerm, string userKey)
        {
            return context.Rooms.Where(x => x.UserKey==userKey &&( searchTerm == "All" ||  x.RoomName.Contains(searchTerm) || x.RoomKey.Contains(searchTerm) || x.RoomDesc.Contains(searchTerm))).OrderBy(x => x.RoomName);
        }

        public Room Update(Room updateRoom,string userKey)
        {
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var room = context.Rooms.Attach(updateRoom);
                room.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChangesAsync();
            }
            return updateRoom;
        }
    }
}
