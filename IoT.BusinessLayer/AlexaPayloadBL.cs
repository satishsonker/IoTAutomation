using System;
using IoT.ModelLayer;
using IoT.DataLayer.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class AlexaPayloadBL
    {
        private readonly IAlexaPayload _alexaPayload;
        public AlexaPayloadBL(IAlexaPayload alexaPayload)
        {
            _alexaPayload = alexaPayload;
        }

        public IEnumerable<Device> GetAlexaDiscoveryPayload(string userKey)
        {
            return _alexaPayload.GetAlexaDiscoveryPayload(userKey);
        }

        public Task<bool> UpdateDeviceStatus(string deviceKey, string status, string userKey)
        {
           return _alexaPayload.UpdateDeviceStatus(deviceKey, status, userKey);
        }
        public Task<string> GetDeviceStatus(string deviceKey,string userKey)
        {
            return _alexaPayload.GetDeviceStatus(deviceKey, userKey);
        }
    }
}
