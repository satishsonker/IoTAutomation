using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
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
            return _scenes.Update(updateScene, userKey);
        }
    }
}
