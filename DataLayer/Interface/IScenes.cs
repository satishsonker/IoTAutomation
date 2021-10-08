using System;
using System.Collections.Generic;
using System.Text;
using IoT.ModelLayer;

namespace IoT.DataLayer.Interface
{
  public  interface IScenes
    {
        Scene Add(Scene newScene, string userKey);
        Scene Update(Scene updateScene,Scene newScene ,string userKey);
        Scene Delete(string SceneKey, string userKey);
        IEnumerable<Scene> GetAll(string userKey);
        Scene Get(string userKey,string sceneKey);
        IEnumerable<Scene> Search(string searchTerm, string userKey);

    }
}
