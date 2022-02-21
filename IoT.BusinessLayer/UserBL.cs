using System;
using IoT.ModelLayer;
using IoT.DataLayer.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class UserBL
    {
        private readonly IUsers _users;
        //private readonly IEmailSender
        public UserBL(IUsers users)
        {
            _users = users;
        }

        public async Task<User> AddUser(User user)
        {
            if(user!=null)
            {
                user.CreatedDate = DateTime.Now;
            }
         return  await _users.Add(user);
        }
        public User GetAPIKey(string userKey)
        {
            return _users.APIKeyGet(userKey);
        }
        public async Task<User> ResetAPIKey(string userKey)
        {
            return await _users.APIKeyReset(userKey);
        }
        public User GetUser(string userKey)
        {
            return _users.GetUser(userKey);
        }
        public async Task<User> UpdateUser(User user)
        {
            return await _users.Update(user);
        }
        public  Task<UserPermission> GetUserPermission(string userKey)
        {
            return  _users.GetUserPermission(userKey);
        }
        public async Task<PagingRecord> GetAllUserPermissions(int pageNo, int pageSize, string userKey)
        {
            return await _users.GetAllUserPermissions(pageNo,pageSize,userKey);
        }

        public async Task<bool> CheckUser(string userName,string password)
        {

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return false;
            else
                return await _users.CheckUser(userName, password);
        }
       public async Task<PagingRecord> SearchUsers(string searchTerm, int pageNo, int pageSize, string userKey)
        {
            return await _users.SearchUsers(searchTerm, pageNo, pageSize, userKey);
        }
      public async  Task<PagingRecord> SearchPermissions(string searchTerm, int pageNo, int pageSize, string userKey)
        {
            return await _users.SearchPermissions(searchTerm, pageNo, pageSize, userKey);
        }
    }
}
