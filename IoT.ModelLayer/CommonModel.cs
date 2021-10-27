using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace IoT.ModelLayer
{
    public class SharedTableModel:SharedTableModelNoUserKey
    {
        public string UserKey { get; set; }
    }
    public class SharedTableModelNoUserKey
    {
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
    }
    public class ResponseModel
    {
        public string Message { get; set; }
        public MessageTypes MessageType { get; set; }
        public dynamic Data { get; set; }
    }
    public enum MessageTypes
    {
        Saved,
        Updated,
        Deleted,
        Error,
        Warning,
        Duplicate,
        NotSaved,
        NotUpdated,
        NotDeleted,
        NoData,
        ValidationIssue,
        UserkeyNotProvided,
        Unauthorized,
        General
    }
}
