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
        User Update(User updateUser);
        User Delete(string userKey);
        IEnumerable<User> GetAllUsers();
        User GetUser(string userKey);
        User APIKeyGet(string userKey);
        User APIKeyReset(string userKey);
        IEnumerable<User> SearchUsers(string searchTerm);
        Task<UserPermission> GetUserPermission(string userKey);
        Task<List<UserPermission>> GetAllUserPermissions(string userKey);
        Task<bool> CheckUser(string userName, string password);
    }
}
