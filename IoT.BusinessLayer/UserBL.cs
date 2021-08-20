using System;
using IoT.DataLayer.Models;
using IoT.DataLayer.Repository;
using IoT.DataLayer.Interface;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
