using IoT.DataLayer.Interface;
using IoT.ModelLayer;
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

        public async Task<List<Device>> GetAlexaDiscoveryPayload(string userKey)
        {
            return await context.Devices.Include(x => x.DeviceType).ThenInclude(x => x.DeviceCapabilities).ToListAsync();
        }

        public async Task<string> GetDeviceStatus(string deviceKey, string userKey)
        {
            if (string.IsNullOrEmpty(deviceKey))
                return "Unknown";
            else
            {
                var result = await context.Devices.Where(x => x.DeviceKey == deviceKey).Select(x => x.Status).FirstOrDefaultAsync();
                if (result == null)
                    return "Unknown";
                return result;
            }
        }

        public async Task<bool> UpdateDeviceStatus(string deviceKey, string status, string userKey)
        {
            bool result = false;
            var data = await context.Devices.Where(x => x.DeviceKey == deviceKey).FirstOrDefaultAsync();
            if (data != null)
            {
                data.ModifiedDate = DateTime.Now;
                data.LastConnected = DateTime.Now;
                data.ConnectionCount += 1;
                data.Status = status;
                var entity = context.Entry(data);
                entity.State = EntityState.Modified;
                if (await context.SaveChangesAsync() > 0)
                    result = true;
            }
            return result;
        }
    }
}
