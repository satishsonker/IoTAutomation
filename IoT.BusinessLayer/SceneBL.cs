using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
   public class SceneBL
    {
        private readonly IScenes _scenes;
        public SceneBL(IScenes scenes)
        {
            _scenes = scenes;
        }

        public async Task<Scene> Add(Scene newScene, string userKey)
        {
            return await _scenes.Add(newScene, userKey);
        }

        public async Task<Scene> Delete(string sceneKey, string userKey)
        {
            return await _scenes.Delete(sceneKey, userKey);
        }

        public async Task<Scene> Get(string userKey, string sceneKey)
        {
            return await _scenes.Get(userKey, sceneKey);
        }

        public async Task<PagingRecord> GetAll(string userKey,int pageNo,int pageSize)
        {
            return await _scenes.GetAll(userKey,pageNo,pageSize);
        }

        public async Task<IEnumerable<Scene>> Search(string searchTerm, string userKey)
        {
            return await _scenes.Search(searchTerm, userKey);
        }

        public async Task<Scene> Update(Scene updateScene, string userKey)
        {
            var newScene = new Scene
            {
                CreatedDate = updateScene.CreatedDate,
                SceneDesc = updateScene.SceneDesc,
                SceneName = updateScene.SceneName,
                UserKey = userKey,
                SceneKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper()
            };
            var newSceneList = new List<SceneAction>();
            foreach (var item in updateScene.SceneActions)
            {
                SceneAction newSceneAction = new SceneAction
                {
                    CreatedDate = newScene.CreatedDate,
                    SceneActionKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                    UserKey = userKey,
                    Action = item.Action,
                    Value = item.Value,
                    SceneId = item.SceneId,
                    DeviceId = item.DeviceId
                };
                newSceneList.Add(newSceneAction);
                item.Device = null;
            }
            newScene.SceneActions = newSceneList;
            return await _scenes.Update(updateScene,newScene, userKey);
        }
    }
}
