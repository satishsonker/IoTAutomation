using IoT.DataLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class AlexaEventSourceRepository : IAlexaEventSource
    {
        private readonly AppDbContext context;
        public AlexaEventSourceRepository(AppDbContext context)
        {
            this.context = context;
        }
        public string GetRefreshToken()
        {
            return context.SkillTokens.Select(x => x.RefreshToken).FirstOrDefault();
        }

        public Tuple<string, string, DateTime> GetToken()
        {
            var data= context.SkillTokens.Select(x => x).FirstOrDefault();
            if(data!=null)
            {
               return new Tuple<string, string, DateTime>(data.Token, data.RefreshToken, data.ExpireAt);
            }
            return new Tuple<string, string, DateTime>(string.Empty, string.Empty, DateTime.Now); ;
        }

        public void UpdateCode(string code, string userKey)
        {
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var oldData = context.SkillTokens.FirstOrDefault();
                if (oldData != null)
                {
                    oldData.Code = code;
                    oldData.ModifiedDate = DateTime.Now;
                    var entity = context.Attach(oldData);
                    entity.State = EntityState.Modified;
                    context.SaveChangesAsync();
                }
            }
        }

        public void UpdateToken(string token, string refreshToken, int expireMin,string userKey)
        {
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var oldData = context.SkillTokens.FirstOrDefault();
                if (oldData != null)
                {
                    oldData.Token = token;
                    oldData.RefreshToken = refreshToken;
                    oldData.ModifiedDate = DateTime.Now;
                    oldData.ExpireAt = DateTime.Now.AddMinutes(expireMin);
                    var entity = context.Attach(oldData);
                    entity.State = EntityState.Modified;
                    context.SaveChangesAsync();
                }
            }
        }

        public bool VerifyAPIKey(string apiKey)
        {
            return context.Users.Where(x => x.APIKey == apiKey).Count() > 0;
        }
    }
}
