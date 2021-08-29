using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SceneController : ControllerBase
    {
        private SceneBL _sceneBL;
        public SceneController(IScenes scenes)
        {
            _sceneBL = new SceneBL(scenes);
        }

        [HttpPost]
        [Route("AddScene")]
        public IActionResult AddScene([FromBody] Scene Scene, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newScene = _sceneBL.Add(Scene, userKey);
                    if (newScene.SceneId > 0)
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
        [Route("GetAllScene")]
        public IActionResult GetAllScene([FromHeader] string userKey)
        {
            try
            {
                return Ok(_sceneBL.GetAll(userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("GetScene")]
        public IActionResult GetAllScene([FromQuery] string SceneKey, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_sceneBL.Get(SceneKey, userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("SearchScene")]
        public IActionResult SearchScene([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_sceneBL.Search(searchTerm, userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
     
        [HttpDelete]
        [Route("DeleteScene")]
        public IActionResult DeleteScene([FromQuery] string SceneKey, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_sceneBL.Delete(SceneKey, userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("UpdateScene")]
        public IActionResult UpdateScene([FromBody] Scene Scene, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_sceneBL.Update(Scene, userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
