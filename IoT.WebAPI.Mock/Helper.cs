using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.WebAPI.Mock
{
    public class Helper
    {
        public static string UserKey { get; } = "100965730392215373474";
        public static string ApiKey { get; } = "100965730392215373474";
        public static string SearchTerm { get; } = "Test";
        public static int DeleteId { get; } = 1;
        public static string EndpointId
        {
            get { return Guid.NewGuid().ToString().ToUpper().Replace("-", ""); }
        }

    }
}
