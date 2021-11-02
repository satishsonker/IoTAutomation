using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IoT.ModelLayer.Alexa
{
  public  class DoorbellEventModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        [DisplayName("event")]
        public Event @event { get; set; }
    }
    public class Header
    {
        public string messageId { get; set; }
        [DisplayName("namespace")]
        public string @namespace { get; set; }
        public string name { get; set; }
        public string payloadVersion { get; set; }
    }

    public class Scope
    {
        public string type { get; set; }
        public string token { get; set; }
    }

    public class Endpoint
    {
        public Scope scope { get; set; }
        public string endpointId { get; set; }
    }

    public class Cause
    {
        public string type { get; set; }
    }

    public class Payload
    {
        public Cause cause { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Event
    {
        public Header header { get; set; }
        public Endpoint endpoint { get; set; }
        public Payload payload { get; set; }
    }

}
