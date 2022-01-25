using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.ModelLayer;

namespace IoT.DataLayer.Interface
{
  public  interface IScenes
    {
       Task<Scene> Add(Scene newScene, string userKey);
        Task<Scene> Update(Scene updateScene,Scene newScene ,string userKey);
        Task<Scene> Delete(string SceneKey, string userKey);
        Task<PagingRecord> GetAll(string userKey,int pageNo,int pageSize);
        Task<Scene> Get(string userKey,string sceneKey);
        Task<IEnumerable<Scene>> Search(string searchTerm, string userKey);

    }
}
