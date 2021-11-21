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

        public async Task<List<Device>> GetAlexaDiscoveryPayload(string userKey)
        {
            return await _alexaPayload.GetAlexaDiscoveryPayload(userKey);
        }

        public async Task<bool> UpdateDeviceStatus(string deviceKey, string status, string userKey)
        {
           return await _alexaPayload.UpdateDeviceStatus(deviceKey, status, userKey);
        }
        public async Task<string> GetDeviceStatus(string deviceKey,string userKey)
        {
            return await _alexaPayload.GetDeviceStatus(deviceKey, userKey);
        }
    }
}
