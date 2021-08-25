﻿using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
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
            if (user == null)
            {
                context.Users.AddAsync(newUser);
                context.SaveChanges();
            }
            else
            {
                user.LastLogin = DateTime.Now;
                var updatedUser = context.Users.Attach(user);
                updatedUser.State = EntityState.Modified;
                context.SaveChanges();
            }
            return newUser;
        }

        public User APIKeyGet(string userKey)
        {
            var user = context.Users.Where(x => x.UserKey == userKey).FirstOrDefault();
            if(user!=null)
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
                return new User() { APIKey=user.APIKey,ModifiedDate=user.ModifiedDate};
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
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Users;
            }
            return context.Users.Where(x => x.FirstName.Contains(searchTerm) || x.Email.Contains(searchTerm) || x.UserKey.Contains(searchTerm));
            
        }

        public User Update(User updateUser)
        {
            var user = context.Users.Attach(updateUser);
            user.State =EntityState.Modified;
            context.SaveChangesAsync();
            return updateUser;
        }
    }
}
