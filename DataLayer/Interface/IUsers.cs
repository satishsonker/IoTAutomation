using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Interface
{
  public interface IUsers
    {
        User Add(User newUser);
        User Update(User updateUser);
        User Delete(string userKey);
        IEnumerable<User> GetAllUsers();
        User GetUser(string userKey);
        IEnumerable<User> SearchUsers(string searchTerm);
    }
}
