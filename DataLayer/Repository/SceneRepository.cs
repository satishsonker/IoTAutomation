using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Repository
{
    public class SceneRepository : IScenes
    {
        private readonly AppDbContext context;
        public SceneRepository(AppDbContext _context)
        {
            this.context = _context;
        }
        public async Task<Scene> Add(Scene newScene, string userKey)
        {
            if (await context.Users.Where(x => x.UserKey == userKey).CountAsync() > 0)
            {
                var scene = await context.Scenes.Where(x => x.SceneId == newScene.SceneId).FirstOrDefaultAsync();
                if (scene == null)
                {
                    newScene.CreatedDate = DateTime.Now;
                    newScene.ModifiedDate = DateTime.Now;
                    newScene.SceneKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                    newScene.UserKey = userKey;
                    foreach (SceneAction item in newScene.SceneActions)
                    {
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        item.UserKey = userKey;
                        item.SceneActionKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                        item.SceneActionId = 0;
                    }
                    context.Scenes.Add(newScene);
                    await context.SaveChangesAsync();

                    return newScene;
                }
                return new Scene();
            }
            return new Scene();
        }

        public async Task<Scene> Delete(string SceneKey, string userKey)
        {
            var deleteScene = await context.Scenes.Where(x => x.SceneKey == SceneKey && x.UserKey == userKey).FirstOrDefaultAsync();
            if (deleteScene != null)
            {
                var oldScene = context.Scenes.Attach(deleteScene);
                oldScene.State = EntityState.Deleted;
                await context.SaveChangesAsync();
                return deleteScene;
            }
            return new Scene();
        }

        public async Task<PagingRecord> GetAll(string userKey, int pageNo, int pageSize)
        {
            PagingRecord result = new PagingRecord();
            var totalRecord = await context.Scenes
            .Where(x => x.UserKey == userKey)
            .Include(x => x.SceneActions)
            .OrderBy(x => x.SceneName)
            .ToListAsync();
            result.Data = totalRecord.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList();
            result.PageNo = pageNo;
            result.PageSize = pageSize;
            result.TotalRecord = totalRecord.Count;
            return result;

        }

        public async Task<Scene> Get(string userKey, string sceneKey)
        {
            return await context.Scenes
                .Where(x => x.UserKey == userKey && x.SceneKey == sceneKey)
                .Include(x => x.SceneActions)
                .ThenInclude(x => x.Device)
                .ThenInclude(x => x.DeviceType)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Scene>> Search(string searchTerm, string userKey)
        {
            searchTerm = searchTerm.ToUpper();
            return await context.Scenes
                .Where(x =>
                            (searchTerm == "All" ||
                                x.SceneKey.ToUpper().Contains(searchTerm) ||
                                x.SceneName.ToUpper().Contains(searchTerm) ||
                                x.SceneDesc.Contains(searchTerm)
                            ) && x.UserKey == userKey)
                .OrderBy(x => x.SceneName)
                .ToListAsync();


        }

        public async Task<Scene> Update(Scene updateScene, Scene newScene, string userKey)
        {
            try
            {
                context.Database.OpenConnection();
                if ( context.Users.Where(x => x.UserKey == userKey).Count() > 0)
                {

                    var scene = context.Scenes.Attach(updateScene);
                    var oldAction = context.SceneActions
                        .Where(x => updateScene.SceneActions.Select(y => y.SceneId)
                        .Contains(x.SceneId))
                        .ToList();

                    context.SceneActions.RemoveRange(oldAction);

                    await context.SaveChangesAsync();
                    updateScene.SceneActions = null;
                    context.Scenes.Remove(updateScene);

                    await context.SaveChangesAsync();
                    newScene.SceneId = 0;
                    context.Scenes.Add(newScene);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return updateScene;
        }
    }
}
