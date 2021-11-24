using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private RoomBL _roomBL;
        public RoomController(IRooms rooms)
        {
            _roomBL = new RoomBL(rooms);
           
        }

        [HttpPost]
        [Route("AddRoom")]
        public async Task<Room> AddRoom([FromBody] Room room, [FromHeader] string userKey)
        {
            Room newRoom = new Room();
            try
            {
                if (ModelState.IsValid)
                {
                    newRoom =await _roomBL.AddRoom(room,userKey);
                    if (newRoom.RoomId > 0)
                        return newRoom;
                    return newRoom;
                }
                return newRoom;
            }
            catch (Exception)
            {
                return newRoom;
            }     

        }

        [HttpGet]
        [Route("GetAllRoom")]
        public async Task<List<Room>> GetAllRoom([FromHeader]string userKey)
        {
            try
            {
                return await _roomBL.GetAllRoom(userKey);
            }
            catch (Exception ex)
            {
                return new List<Room>();
            }

        }
        [HttpGet]
        [Route("GetRoom")]
        public async Task<Room> GetAllRoom([FromQuery]string roomKey, [FromHeader] string userKey)
        {
            try
            {
                return await _roomBL.GetRoom(roomKey,userKey);
            }
            catch (Exception ex)
            {
                return new Room();
            }

        }
        [HttpGet]
        [Route("SearchRoom")]
        public async Task<List<Room>> SearchRoom([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                return await _roomBL.SearchRoom(searchTerm, userKey);
            }
            catch (Exception ex)
            {
                return new List<Room>();
            }

        }
        [HttpGet]
        [Route("GetRoomDropdown")]
        public async Task<List<object>> GetRoomDropdown([FromHeader] string userKey)
        {
            try
            {
                return await _roomBL.GetRoomDropdown(userKey);
            }
            catch (Exception ex)
            {
                return new List<object>();
            }

        }
        [HttpDelete]
        [Route("DeleteRoom")]
        public async Task<Room> DeleteRoom([FromQuery] string roomKey, [FromHeader] string userKey)
        {
            try
            {
                return await _roomBL.DeleteRoom(roomKey, userKey);
            }
            catch (Exception ex)
            {
                return new Room();
            }

        }

        [HttpPost]
        [Route("UpdateRoom")]
        public async Task<Room> UpdateRoom([FromBody] Room room, [FromHeader] string userKey)
        {
            try
            {
                return await _roomBL.UpdateRoom(room, userKey);
            }
            catch (Exception ex)
            {
                return new Room();
            }

        }

    }
}