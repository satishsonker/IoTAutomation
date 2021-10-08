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
        public IActionResult AddRoom([FromBody] Room room, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newRoom = _roomBL.AddRoom(room,userKey);
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
        public IActionResult GetAllRoom([FromHeader]string userKey)
        {
            try
            {
                return Ok(_roomBL.GetAllRoom(userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetRoom")]
        public IActionResult GetAllRoom([FromQuery]string roomKey, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_roomBL.GetRoom(roomKey,userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("SearchRoom")]
        public IActionResult SearchRoom([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_roomBL.SearchRoom(searchTerm, userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetRoomDropdown")]
        public IActionResult GetRoomDropdown([FromHeader] string userKey)
        {
            try
            {
                return Ok(_roomBL.GetRoomDropdown(userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Route("DeleteRoom")]
        public IActionResult DeleteRoom([FromQuery] string roomKey, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_roomBL.DeleteRoom(roomKey, userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("UpdateRoom")]
        public IActionResult UpdateRoom([FromBody] Room room, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_roomBL.UpdateRoom(room, userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}