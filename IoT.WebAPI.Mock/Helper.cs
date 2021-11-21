using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.WebAPI.Mock
{
    public class Helper
    {
        public static string UserKey { get; } = "100965730392215373474";
        public static string ApiKey { get; } = "100965730392215373474";
        public static string AlexaEventUrl { get; set; } = "https://api.eu.amazonalexa.com/v3/events";
        public static string SearchTerm { get; } = "Test";
        public static int DeleteId { get; } = 1;
        public static string EndpointId
        {
            get { return "4BBE91702F5F4BDB9C3DE49EDF13BEDC"; }
        }

    }
}
