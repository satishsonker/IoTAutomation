using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class ActivityLogRepository : IActivityLogs
    {
        private readonly AppDbContext context;
        public ActivityLogRepository(AppDbContext _context)
        {
            context = _context;

        }
        public ActivityLog Add(ActivityLog entity, string userKey)
        {
            if (entity != null)
            {
                List<ActivityLog> oldLogs = context.ActivityLogs.Where(x => x.UserKey == userKey).OrderByDescending(x=>x.CreatedDate).ToList();
                if(oldLogs.Count()>100)
                {
                    var data = context.ActivityLogs.Attach(oldLogs.Skip(100).FirstOrDefault());
                    data.State = EntityState.Deleted;
                    context.SaveChanges();
                }
                if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
                {
                    context.ActivityLogs.Add(entity);
                    context.SaveChangesAsync();
                    return entity;
                }
            }
            return new ActivityLog();
        }

        public int Delete(ActivityLog entity, string userKey)
        {
            if (entity != null)
            {
                if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
                {
                   var data= context.ActivityLogs.Attach(entity);
                    data.State = EntityState.Deleted;
                    context.SaveChangesAsync();
                    return context.SaveChanges();
                }
            }
            return 0;
        }

        public IEnumerable<ActivityLog> GetAll(string userKey)
        {
            return context.ActivityLogs.Where(x => x.UserKey == userKey).OrderByDescending(x => x.CreatedDate).ToList();
        }
    }
}
