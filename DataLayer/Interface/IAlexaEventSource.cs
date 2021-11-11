using IoT.ModelLayer.Alexa;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
  public  interface IAlexaEventSource
    {
        Task<bool> UpdateCode(string code, string userKey);
        Task<bool> UpdateToken(string token,string refreshToken, int expireMin, string userKey);
        Task<Tuple<string,string, DateTime>> GetToken();
        Task<SkillToken> GetToken(bool allField);
        Task<string> GetRefreshToken();
        Task<bool> VerifyAPIKey(string apiKey);
    }
}
