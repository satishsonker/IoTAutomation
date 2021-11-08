using IoT.ModelLayer.Alexa;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Interface
{
  public  interface IAlexaEventSource
    {
        void UpdateCode(string code, string userKey);
        void UpdateToken(string token,string refreshToken, int expireMin, string userKey);
        Tuple<string,string, DateTime> GetToken();
        SkillToken GetToken(bool allField);
        string GetRefreshToken();
        bool VerifyAPIKey(string apiKey);
    }
}
