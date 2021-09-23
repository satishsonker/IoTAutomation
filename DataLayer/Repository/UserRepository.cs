using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
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
        public Task<User> Add(User newUser)
        {
            try
            {
                var user = context.Users.Where(x => x.UserKey == newUser.UserKey).FirstOrDefault();
                if (user == null)
                {

                    newUser.UserKey = newUser.UserKey.ToUpper();
                    context.Users.Add(newUser);
                    context.SaveChangesAsync().ContinueWith(t =>
                    {
                        if (t.Result > 0)
                        {
                            UserPermission permission = new UserPermission()
                            {
                                UserId = newUser.UserId,
                                UserKey = newUser.UserKey.ToUpper(),
                                CanView = true,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };
                            int permissionId = 0;
                            var allPermisiionIds = context.UserPermissions.Select(x => x.UserPermissionId).ToListAsync();
                            if (allPermisiionIds != null && allPermisiionIds.Result.Count() > 0)
                            {
                                permissionId = allPermisiionIds.Result.Max(x => x);
                            }
                            permission.UserPermissionId = permissionId + 1;
                            context.UserPermissions.Add(permission);
                            context.SaveChangesAsync();
                        }
                    });

                }
                else
                {
                    user.LastLogin = DateTime.Now;
                    var updatedUser = context.Users.Attach(user);
                    updatedUser.State = EntityState.Modified;
                    context.SaveChangesAsync();
                }
                newUser.UserPermissions = context.UserPermissions.Where(x => x.UserKey == newUser.UserKey).ToListAsync().Result;
                return Task<User>.Factory.StartNew(() => newUser);
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

        public User APIKeyReset(string userKey)
        {
            var user = context.Users.Where(x => x.UserKey == userKey).FirstOrDefault();
            if (user != null)
            {

                user.APIKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper() + Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                user.ModifiedDate = DateTime.Now;
                var updateduser = context.Users.Attach(user);
                updateduser.State = EntityState.Modified;
                context.SaveChanges();
                return new User() { APIKey = user.APIKey, ModifiedDate = user.ModifiedDate };
            }
            return new User();
        }

        public User Delete(string userKey)
        {
            var user = context.Users.FirstOrDefault(x => x.UserKey == userKey);
            context.Users.Remove(user);
            context.SaveChangesAsync();
            return user;
        }

        public IEnumerable<UserPermission> GetAllUserPermissions(string userKey)
        {
            if (GetUserPermission(userKey) != null)
            {
                var data = context.UserPermissions.Include(x => x.User).OrderBy(x => x.User.FirstName).ToList();
                foreach (UserPermission userPermission in data)
                {
                    userPermission.User.UserPermissions = null;
                }
                return data;
            }
            return new List<UserPermission>();
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
            var data = await context.UserPermissions.Where(x => x.UserKey == userKey).FirstOrDefaultAsync();
            return data;
        }

        public IEnumerable<User> SearchUsers(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Users;
            }
            return context.Users.Where(x => x.FirstName.Contains(searchTerm) || x.Email.Contains(searchTerm) || x.UserKey.Contains(searchTerm));

        }

        public User Update(User updateUser)
        {
            var user = context.Users.Attach(updateUser);
            user.State = EntityState.Modified;
            context.SaveChangesAsync();
            return updateUser;
        }
    }
}
