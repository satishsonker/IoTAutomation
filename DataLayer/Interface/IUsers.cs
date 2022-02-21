using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
  public interface IUsers
    {
        Task<User> Add(User newUser);
        Task<User> Update(User updateUser);
        User Delete(string userKey);
        IEnumerable<User> GetAllUsers();
        User GetUser(string userKey);
        User APIKeyGet(string userKey);
        Task<User> APIKeyReset(string userKey);
        Task<PagingRecord> SearchUsers(string searchTerm,int pageNo, int pageSize, string userKey);
        Task<PagingRecord> SearchPermissions(string searchTerm, int pageNo, int pageSize, string userKey);
        Task<UserPermission> GetUserPermission(string userKey);
        Task<PagingRecord> GetAllUserPermissions(int pageNo,int pageSize, string userKey);
        Task<bool> CheckUser(string userName, string password);
        Task<bool> CheckUserAsync(string userKey);
        bool CheckUser(string userKey);
    }
}
