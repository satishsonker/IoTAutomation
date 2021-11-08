using IoT.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class MqttRepository : IMqtt
    {
        private readonly AppDbContext context;
        public MqttRepository(AppDbContext _context)
        {
            context = _context;
        }
        public string[] GetSubscribeTopic()
        {
          return  context.Users.Where(x=>x.APIKey.Length>0).Select(x => x.APIKey).ToArray();
        }
    }
}
