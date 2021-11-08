using IoT.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.BusinessLayer
{
  public  class MqttBL
    {
        private readonly IMqtt imqtt;
        public MqttBL(IMqtt _imqtt)
        {
            imqtt = _imqtt;
        }

        public string[] GetSubscribeTopic()
        {
           return imqtt.GetSubscribeTopic();
        }
    }
}
