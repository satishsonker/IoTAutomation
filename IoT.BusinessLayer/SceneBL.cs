using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.BusinessLayer
{
   public class SceneBL
    {
        private readonly IScenes _scenes;
        public SceneBL(IScenes scenes)
        {
            _scenes = scenes;
        }

        public Scene Add(Scene newScene, string userKey)
        {
            return _scenes.Add(newScene, userKey);
        }

        public Scene Delete(string sceneKey, string userKey)
        {
            return _scenes.Delete(sceneKey, userKey);
        }

        public Scene Get(string userKey, string sceneKey)
        {
            return _scenes.Get(userKey, sceneKey);
        }

        public IEnumerable<Scene> GetAll(string userKey)
        {
            return _scenes.GetAll(userKey);
        }

        public IEnumerable<Scene> Search(string searchTerm, string userKey)
        {
            return _scenes.Search(searchTerm, userKey);
        }

        public Scene Update(Scene updateScene, string userKey)
        {
            var newScene = new Scene();
            newScene.CreatedDate = updateScene.CreatedDate;
            newScene.SceneDesc = updateScene.SceneDesc;
            newScene.SceneName = updateScene.SceneName;
            newScene.UserKey = userKey;
            newScene.SceneKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            var newSceneList = new List<SceneAction>();
            foreach (var item in updateScene.SceneActions)
            {
                SceneAction newSceneAction = new SceneAction();
                newSceneAction.CreatedDate = newScene.CreatedDate;
                newSceneAction.SceneActionKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                newSceneAction.UserKey = userKey;
                newSceneAction.Action = item.Action;
                newSceneAction.Value = item.Value;
                newSceneAction.SceneId = item.SceneId;
                newSceneAction.DeviceId = item.DeviceId;
                newSceneList.Add(newSceneAction);
                item.Device = null;
            }
            newScene.SceneActions = newSceneList;
            return _scenes.Update(updateScene,newScene, userKey);
        }
    }
}
