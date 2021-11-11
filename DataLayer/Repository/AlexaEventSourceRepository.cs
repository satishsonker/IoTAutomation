using IoT.DataLayer.Interface;
using IoT.ModelLayer.Alexa;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Repository
{
    public class AlexaEventSourceRepository : IAlexaEventSource
    {
        private readonly AppDbContext context;
        private readonly ILogger<AlexaEventSourceRepository> _logger;
        public AlexaEventSourceRepository(AppDbContext context, ILogger<AlexaEventSourceRepository> logger)
        {
            this.context = context;
            this._logger = logger;
        }
        public async Task<string> GetRefreshToken()
        {
            try
            {
                return await context.SkillTokens.Select(x => x.RefreshToken).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while GetRefreshToken", ex.Message);
                return string.Empty;
            }
        }

        public async Task<Tuple<string, string, DateTime>> GetToken()
        {
            var result=new Tuple<string, string, DateTime>(string.Empty, string.Empty, DateTime.Now);
            try
            {
                var data = await context.SkillTokens.Select(x => x).FirstOrDefaultAsync();
                if (data != null)
                {
                    return new Tuple<string, string, DateTime>(data.Token, data.RefreshToken, data.ExpireAt);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while GetToken", ex.Message);
                return result;
            }
        }

        public async Task<SkillToken> GetToken(bool allField)
        {
            try
            {
                return await context.SkillTokens.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while Get Alexa Token with all field in AlexaEventSourceRepository", ex.Message);
                return new SkillToken();
            }
        }

        public async Task<bool> UpdateCode(string code, string userKey)
        {
            try
            {
                if (await context.Users.Where(x => x.UserKey == userKey || x.APIKey == userKey).CountAsync() > 0)
                {
                    var oldData = await context.SkillTokens.FirstOrDefaultAsync();
                    if (oldData != null)
                    {
                        oldData.Code = code;
                        oldData.ModifiedDate = DateTime.Now;
                        var entity = context.Attach(oldData);
                        entity.State = EntityState.Modified;
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while UpdateCode", ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateToken(string token, string refreshToken, int expireMin, string userKey)
        {
            try
            {
                if (userKey == "ByPassApiKey" || await context.Users.Where(x => x.APIKey == userKey).CountAsync() > 0)
                {
                    var oldData = await context.SkillTokens.FirstOrDefaultAsync();
                    if (oldData != null)
                    {
                        oldData.Token = token;
                        oldData.RefreshToken = refreshToken;
                        oldData.ModifiedDate = DateTime.Now;
                        oldData.ExpireAt = DateTime.Now.AddMinutes(expireMin);
                        var entity = context.Attach(oldData);
                        entity.State = EntityState.Modified;
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while Update Token", ex.Message);
                return false;
            }
        }

        public async Task<bool> VerifyAPIKey(string apiKey)
        {
            try
            {
                return await context.Users.Where(x => x.APIKey == apiKey).CountAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while VerifyAPIKey", ex.Message);
                return false;
            }
        }
    }
}
