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
    public class UserRepository : IUsers
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<User> Add(User newUser)
        {
            try
            {
                var user = await context.Users.Where(x => x.UserKey == newUser.UserKey).FirstOrDefaultAsync();
                if (user == null)
                {
                    newUser.UserKey = newUser.UserKey.ToUpper();
                    newUser.APIKey = GenerateApiKey();
                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();
                    UserPermission permission = new UserPermission()
                    {
                        UserId = newUser.UserId,
                        UserKey = newUser.UserKey.ToUpper(),
                        CanView = true,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    context.UserPermissions.Add(permission);

                    await context.SaveChangesAsync();
                    await APIKeyReset(newUser.UserKey);

                }
                else
                {
                    user.LastLogin = DateTime.Now;
                    user.UserPermissions = null;
                    var updatedUser = context.Users.Attach(user);
                    updatedUser.State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                newUser.UserPermissions = context.UserPermissions.Where(x => x.UserKey == newUser.UserKey).ToListAsync().Result;
                foreach (var item in newUser.UserPermissions)
                {
                    item.User = null;
                }
                return newUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User APIKeyGet(string userKey)
        {
            var user = context.Users.Where(x => x.UserKey == userKey).FirstOrDefault();
            if (user != null)
            {
                return new User() { APIKey = user.APIKey, ModifiedDate = user.ModifiedDate };
            }
            return new User();
        }

        public async Task<User> APIKeyReset(string userKey)
        {
            var user =await context.Users.Where(x => x.UserKey == userKey).FirstOrDefaultAsync();
            if (user != null)
            {
                user.APIKey = GenerateApiKey();
                user.ModifiedDate = DateTime.Now;
                var updateduser = context.Users.Attach(user);
                updateduser.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return new User() { APIKey = user.APIKey, ModifiedDate = user.ModifiedDate };
            }
            return new User();
        }

        public async Task<bool> CheckUser(string userName, string password)
        {
            return await context.Users.Where(x => x.Email.ToLower() == userName.ToLower() && x.Password == password).CountAsync() > 0 ? true : false;
        }

        public bool CheckUser(string userKey)
        {
            return context.Users.Where(x => x.UserKey == userKey).ToList().Count > 0;
        }

        public async Task<bool> CheckUserAsync(string userKey)
        {
            var result = await context.Users.Where(x => x.UserKey == userKey).ToListAsync();
            return result.Count > 0;
        }

        public User Delete(string userKey)
        {
            var user = context.Users.FirstOrDefault(x => x.UserKey == userKey);
            context.Users.Remove(user);
            context.SaveChangesAsync();
            return user;
        }

        public async Task<PagingRecord> GetAllUserPermissions(int pageNo, int pageSize, string userKey)
        {
            if (await context.Users.Where(x => x.UserKey == userKey).CountAsync() > 0)
            {
                PagingRecord pagingRecord = new PagingRecord();
                var data = await context.Users.Include(x => x.UserPermissions).OrderBy(x => x.FirstName).ToListAsync();
                foreach (var user in data)
                {
                    foreach (var item in user.UserPermissions)
                    {
                        item.User = null;
                    }
                }
                pagingRecord.Data = data.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList();
                pagingRecord.PageNo = pageNo;
                pagingRecord.PageSize = pageSize;
                pagingRecord.TotalRecord = data.Count;
                return pagingRecord;
            }
            return new PagingRecord();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users;
        }

        public User GetUser(string userKey)
        {
            return context.Users.FirstOrDefault(x => x.UserKey == userKey);
        }

        public async Task<UserPermission> GetUserPermission(string userKey)
        {
            var data = await context.UserPermissions.Include(x => x.User).Where(x => x.UserKey == userKey).FirstOrDefaultAsync();
            if (data != null && data.User != null && data.User.UserPermissions != null)
                data.User.UserPermissions = null;
            return data;
        }

        public async Task<PagingRecord> SearchPermissions(string searchTerm, int pageNo, int pageSize, string userKey)
        {
            searchTerm = searchTerm.ToLower();
            PagingRecord pagingRecord = new PagingRecord();
            var data = await context.Users.Include(x => x.UserPermissions).OrderBy(x => x.FirstName).ToListAsync();
            pagingRecord.PageNo = pageNo;
            pagingRecord.PageSize = pageSize;
            if (string.IsNullOrEmpty(searchTerm))
            {
                pagingRecord.Data = data.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList();

                pagingRecord.TotalRecord = data.Count;
                return pagingRecord;
            }
            if (searchTerm != "all")
                data = data
                    .Where(x => x.FirstName.Contains(searchTerm) || x.Email.Contains(searchTerm) || x.UserKey.Contains(searchTerm)).ToList();
            pagingRecord.Data = data
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable()
                .Cast<object>()
                .ToList();
            pagingRecord.TotalRecord = data.Count;
            return pagingRecord;
        }

        public async Task<PagingRecord> SearchUsers(string searchTerm, int pageNo, int pageSize, string userKey)
        {
            PagingRecord pagingRecord = new PagingRecord();
            var data = await context.Users.Where(x => x.UserKey == userKey).OrderBy(x => x.FirstName).ToListAsync();
            if (string.IsNullOrEmpty(searchTerm))
            {
                pagingRecord.Data = data.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList();
                pagingRecord.PageNo = pageNo;
                pagingRecord.PageSize = pageSize;
                pagingRecord.TotalRecord = data.Count;
                return pagingRecord;
            }
            if (searchTerm != "all")
                data = data;
            pagingRecord.Data = data
                .Where(x => x.FirstName.Contains(searchTerm) || x.Email.Contains(searchTerm) || x.UserKey.Contains(searchTerm))
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .AsEnumerable()
                .Cast<object>()
                .ToList();
            pagingRecord.TotalRecord = data.Count;
            return pagingRecord;

        }

        public async Task<User> Update(User updateUser)
        {
            if (updateUser != null)
            {
                updateUser.UserPermissions = null;
                var user = context.Users.Attach(updateUser);
                user.State = EntityState.Modified;
               await context.SaveChangesAsync();
                return updateUser;
            }
            return new User();
        }

        private string GenerateApiKey()
        {
          return  Guid.NewGuid().ToString().Replace("-", "").ToUpper() + Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }
    }
}
