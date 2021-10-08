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

        public IEnumerable<Device> GetAlexaDiscoveryPayload(string userKey)
        {
            var data= context.Devices.Include(x => x.DeviceType).ThenInclude(x => x.DeviceCapabilities);
            if(data!=null)
            {
                foreach (Device item in data)
                {
                    item.DeviceType.DeviceActions = null;
                    foreach (DeviceCapability deviceCapability in item.DeviceType.DeviceCapabilities)
                    {
                        deviceCapability.DeviceType = null;
                    }                    
                }
                return data;
            }
            return new List<Device>();
        }

        public Task<string> GetDeviceStatus(string deviceKey, string userKey)
        {
            if (string.IsNullOrEmpty(deviceKey))
                return Task.Factory.StartNew(() => "OFF");
            var data = context.Devices.Where(x => x.DeviceKey == deviceKey).Select(x => x.Status).FirstOrDefault();
            return Task.Factory.StartNew(() => data);
        }

        public Task<bool> UpdateDeviceStatus(string deviceKey, string status, string userKey)
        {
            Task<bool> result =Task.Factory.StartNew(()=>false);
            var data = context.Devices.Where(x => x.DeviceKey == deviceKey).FirstOrDefault();
            if(data!=null)
            {
                data.ModifiedDate = DateTime.Now;
                data.LastConnected = DateTime.Now;
                data.ConnectionCount += 1;
                data.Status = status;
                var entity = context.Entry(data);
                entity.State = EntityState.Modified;
                if(context.SaveChangesAsync().Result>0)
                    result= Task.Factory.StartNew(() => true);
            }
            return result;
        }
    }
}
