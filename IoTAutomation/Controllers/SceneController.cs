using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using IoT.ModelLayer;
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
        public async Task<IActionResult> AddScene([FromBody] Scene Scene, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newScene =await _sceneBL.Add(Scene, userKey);
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
        public async Task<PagingRecord> GetAllScene([FromHeader] string userKey,[FromQuery] int pageNo,[FromQuery] int pageSize)
        {
            try
            {
              return await _sceneBL.GetAll(userKey,pageNo,pageSize);
            }
            catch (Exception ex)
            {
                return new PagingRecord();
            }

        }

        [HttpGet]
        [Route("GetScene")]
        public async Task<Scene> GetScene([FromQuery] string SceneKey, [FromHeader] string userKey)
        {
            try
            {
                return await _sceneBL.Get(userKey,SceneKey);
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
