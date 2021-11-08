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
    public class MotionDetectModel
    {
        public Event @event { get; set; }
        public Context context { get; set; }
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
        public Change change { get; set; }
        public Cause cause { get; set; }
        public DateTime timestamp { get; set; }
    }
    public class Value
    {
        public string value { get; set; }
    }

    public class Event
    {
        public Header header { get; set; }
        public Endpoint endpoint { get; set; }
        public Payload payload { get; set; }
    }
    public class Context
    {
        public List<PropertyContext> properties { get; set; }
    }
    public class Change
    {
        public Cause cause { get; set; }
        public List<Property> properties { get; set; }
    }

    public class Property
    {
        public string @namespace { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public DateTime timeOfSample { get; set; }
        public int uncertaintyInMilliseconds { get; set; }
    }
    public class PropertyContext
    {
        public string @namespace { get; set; }
        public string name { get; set; }
        public Value value { get; set; }
        public DateTime timeOfSample { get; set; }
        public int uncertaintyInMilliseconds { get; set; }
    }

}

public class RefreshTokenResponseModel
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
}
