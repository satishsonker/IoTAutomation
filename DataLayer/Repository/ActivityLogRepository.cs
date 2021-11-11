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
    public class ActivityLogRepository : IActivityLogs
    {
        private readonly AppDbContext context;
        public ActivityLogRepository(AppDbContext _context)
        {
            context = _context;

        }
        public async Task<int> Add(ActivityLog entity, string userKey)
        {
            if (entity != null)
            {
                var oldLogs =await context.ActivityLogs.Where(x => x.UserKey == userKey).OrderByDescending(x=>x.CreatedDate).ToListAsync();
                if (oldLogs.Count > 100)
                {
                    var data = context.ActivityLogs.Attach(oldLogs.Skip(100).FirstOrDefault());
                    data.State = EntityState.Deleted;
                   await context.SaveChangesAsync();
                }
                else
                {
                    context.ActivityLogs.Add(entity);
                  return await  context.SaveChangesAsync();
                }
            }
            return await Task.Factory.StartNew(() => 0);
        }

        //private int Delete(ActivityLog entity, string userKey)
        //{
        //    if (entity != null)
        //    {
        //        if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
        //        {
        //           var data= context.ActivityLogs.Attach(entity);
        //            data.State = EntityState.Deleted;
        //            context.SaveChangesAsync();
        //            return context.SaveChanges();
        //        }
        //    }
        //    return 0;
        //}

        public async Task<List<ActivityLog>> GetAll(string userKey)
        {
            return await context.ActivityLogs.Where(x => x.UserKey == userKey).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }
    }
}
