using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class UserRepository : IUsers
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public User Add(User newUser)
        {
            var user = context.Users.Where(x => x.UserKey == newUser.UserKey).FirstOrDefault();
            if (user==null)
            {
                context.Users.AddAsync(newUser);
                context.SaveChangesAsync();
            }
            else
            {
                user.LastLogin = DateTime.Now;
               var updatedUser =context.Users.Attach(user);
                updatedUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChangesAsync();
            }
            return newUser;
        }

        public User Delete(string userKey)
        {
           var user= context.Users.FirstOrDefault(x => x.UserKey == userKey);
            context.Users.Remove(user);
            context.SaveChangesAsync();
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users;
        }

        public User GetUser(string userKey)
        {
            return context.Users.FirstOrDefault(x => x.UserKey == userKey);
        }

        public IEnumerable<User> SearchUsers(string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm))
            {
                return context.Users;
            }
            return context.Users.Where(x => x.FirstName.Contains(searchTerm) || x.Email.Contains(searchTerm) || x.UserKey.Contains(searchTerm));
        }

        public User Update(User updateUser)
        {
            var user = context.Users.Attach(updateUser);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChangesAsync();
            return updateUser;
        }
    }
}
