using System;
using IoT.DataLayer.Models;
using IoT.DataLayer.Interface;

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
    }
}
