using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
  public  interface IAlexaPayload
    {
       IEnumerable<Device> GetAlexaDiscoveryPayload(string userKey);
       Task<bool> UpdateDeviceStatus(string deviceKey,string status, string userKey);
       Task<string> GetDeviceStatus(string deviceKey,string userKey);
    }
}
