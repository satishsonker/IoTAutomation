using System;
using IoT.DataLayer.Models;
using IoT.DataLayer.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class UserBL
    {
        private readonly IUsers _users;
        public UserBL(IUsers users)
        {
            _users = users;
        }

        public User AddUser(User user)
        {
            if(user!=null)
            {
                user.CreatedDate = DateTime.Now;
            }
            _users.Add(user);
            return user;
        }
        public User GetAPIKey(string userKey)
        {
            return _users.APIKeyGet(userKey);
        }
        public User ResetAPIKey(string userKey)
        {
            return _users.APIKeyReset(userKey);
        }
        public User GetUser(string userKey)
        {
            return _users.GetUser(userKey);
        }
        public User UpdateUser(User user)
        {
            return _users.Update(user);
        }
        public async Task<UserPermission> GetUserPermission(string userKey)
        {
            return await _users.GetUserPermission(userKey);
        }
        public IEnumerable<UserPermission> GetAllUserPermissions(string userKey)
        {
            return _users.GetAllUserPermissions(userKey);
        }
    }
}
