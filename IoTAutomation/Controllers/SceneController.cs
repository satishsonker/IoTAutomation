using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using IoT.ModelLayer;

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
            catch (Exception ex)
            {
                throw ex;
            }


        }

        [HttpGet]
        [Route("GetAllScene")]
        public IEnumerable<Scene> GetAllScene([FromHeader] string userKey)
        {
            try
            {
              return  _sceneBL.GetAll(userKey);
            }
            catch (Exception ex)
            {
                return new List<Scene>();
            }

        }

        [HttpGet]
        [Route("GetScene")]
        public Scene GetScene([FromQuery] string SceneKey, [FromHeader] string userKey)
        {
            try
            {
                return _sceneBL.Get(userKey,SceneKey);
            }
            catch (Exception ex)
            {
                return new Scene();
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
