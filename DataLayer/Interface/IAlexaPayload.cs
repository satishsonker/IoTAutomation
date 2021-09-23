using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Interface
{
  public  interface IAlexaPayload
    {
       IEnumerable<Device> GetAlexaDiscoveryPayload(string userKey);
    }
}
