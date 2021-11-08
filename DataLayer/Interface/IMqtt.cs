using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Interface
{
   public interface IMqtt
    {
        string[] GetSubscribeTopic();
    }
}
