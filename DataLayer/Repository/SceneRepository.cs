using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class SceneRepository : IScenes
    {
        private readonly AppDbContext context;
        public SceneRepository(AppDbContext _context)
        {
            this.context = _context;
        }
        public Scene Add(Scene newScene, string userKey)
        {
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var scene = context.Scenes.Where(x => x.SceneId == newScene.SceneId).FirstOrDefault();
                if (scene == null)
                {
                    newScene.CreatedDate = DateTime.Now;
                    newScene.SceneKey = Guid.NewGuid().ToString();
                    newScene.UserKey = userKey;
                    foreach (SceneAction item in newScene.SceneActions)
                    {
                        item.CreatedDate = DateTime.Now;
                        item.UserKey = userKey;
                        item.SceneActionKey = Guid.NewGuid().ToString();
                        item.SceneActionId = 0;
                    }
                    context.Scenes.Add(newScene);
                    context.SaveChanges();
                   
                    return newScene;
                }
                return new Scene();
            }
            return new Scene();
        }

        public Scene Delete(string SceneKey, string userKey)
        {
            var deleteScene = context.Scenes.Where(x => x.SceneKey == SceneKey && x.UserKey == userKey).FirstOrDefault();
            if (deleteScene != null)
            {
                var oldScene = context.Scenes.Attach(deleteScene);
                oldScene.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.SaveChangesAsync();
                return deleteScene;
            }
            return new Scene();
        }

        public IEnumerable<Scene> GetAll(string userKey)
        {
            return context.Scenes.Where(x => x.UserKey == userKey).Include(x => x.SceneActions).Select(x => x).ToList().OrderBy(x => x.SceneName);
        }

        public Scene Get(string userKey, string sceneKey)
        {
            return context.Scenes.Where(x => x.UserKey == userKey && x.SceneKey==sceneKey).Include(x=>x.SceneActions).ThenInclude(x=>x.Device).ThenInclude(x=>x.DeviceType).Select(x => x).FirstOrDefault();
        }

        public IEnumerable<Scene> Search(string searchTerm, string userKey)
        {
            searchTerm = searchTerm.ToUpper();
            return context.Scenes
                .Where(x => x.UserKey == userKey)
                .Select(x => x).ToList().Where(x => searchTerm == "All" || x.SceneKey.ToUpper().Contains(searchTerm) || x.SceneName.ToUpper().Contains(searchTerm) || x.SceneDesc.Contains(searchTerm) ).OrderBy(x => x.SceneName);


        }

        public Scene Update(Scene updateScene, string userKey)
        {
            if (context.Scenes.Where(x => x.UserKey == userKey).Count() > 0)
            {
                updateScene.ModifiedDate = DateTime.Now;
                var scene = context.Scenes.Attach(updateScene);
                context.SceneActions.AttachRange(updateScene.SceneActions);
                scene.State = EntityState.Modified;
                context.SaveChangesAsync();
            }
            return updateScene;
        }
    }
}
