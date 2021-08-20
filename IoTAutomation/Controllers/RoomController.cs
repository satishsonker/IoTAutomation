using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private RoomBL _roomBL;
        private string _userKey = string.Empty;
        public RoomController(IRooms rooms)
        {
            _roomBL = new RoomBL(rooms);
           
        }

        [HttpPost]
        [Route("AddRoom")]
        public IActionResult AddRoom([FromBody] Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newRoom = _roomBL.AddRoom(room,_userKey);
                    if (newRoom.RoomId > 0)
                        return Ok();
                    return BadRequest(ModelState);
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {

                throw;
            }
           

        }

        [HttpGet]
        [Route("GetAllRoom")]
        public IActionResult GetAllRoom()
        {
            try
            {
                var headerValue = Request.Headers["UserKey"];
                if (headerValue.Any() == true)
                {
                    _userKey = headerValue.ToString();
                }
                return Ok(_roomBL.GetAllRoom(_userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("SearchRoom")]
        public IActionResult SearchRoom([FromQuery] string searchTerm)
        {
            try
            {
                return Ok(_roomBL.SearchRoom(searchTerm, _userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetRoomDropdown")]
        public IActionResult GetRoomDropdown()
        {
            try
            {
                return Ok(_roomBL.GetRoomDropdown(_userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Route("DeleteRoom")]
        public IActionResult DeleteRoom([FromQuery] string roomKey)
        {
            try
            {
                return Ok(_roomBL.DeleteRoom(roomKey, _userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}