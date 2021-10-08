using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.BusinessLayer
{
   public class CommonBL
    {
        internal static ResponseModel GetResponseModel(string msg, MessageTypes messageTypes, dynamic data)
        {
            return new ResponseModel() { Message = msg, MessageType = messageTypes, Data = data };
        }
        internal static ResponseModel GetResponseModel(string msg, MessageTypes messageTypes)
        {
            return GetResponseModel(msg, messageTypes, null);
        }
        internal static ResponseModel GetResponseModel(string msg)
        {
            return GetResponseModel(msg, MessageTypes.General);
        }
        internal static ResponseModel GetResponseModel()
        {
            return GetResponseModel(string.Empty);
        }
    }
}
