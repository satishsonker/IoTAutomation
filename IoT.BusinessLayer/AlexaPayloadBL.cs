using System;
using IoT.DataLayer.Models;
using IoT.DataLayer.Interface;
using System.Collections.Generic;

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
    }
}
