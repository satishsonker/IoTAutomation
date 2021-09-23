using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Repository
{
    public class AlexaPayloadRepository : IAlexaPayload
    {
        private readonly AppDbContext context;

        public AlexaPayloadRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Device> GetAlexaDiscoveryPayload(string userKey)
        {
            var data= context.Devices.Include(x => x.DeviceType).ThenInclude(x => x.DeviceCapabilities);
            return data;
        }
    }
}
