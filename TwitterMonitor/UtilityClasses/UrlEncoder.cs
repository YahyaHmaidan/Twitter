using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TwitterMonitor.UtilityClasses
{
    public class UrlEncoder
    {
        private static string[,] _chars = new string[,]
        {
        { "%", "%25" },     // this is the first one
        { "$" , "%24" },
        { "&", "%26" },
        { "+", "%2B" },
        { ",", "%2C" },
        { "/", "%2F" },
        { ":", "%3A" },
        { ";", "%3B" },
        { "=", "%3D" },
        { "?", "%3F" },
        { "@", "%40" },
        { " ", "%20" },
        { "\"" , "%22" },
        { "<", "%3C" },
        { ">", "%3E" },
        { "#", "%23" },
        { "{", "%7B" },
        { "}", "%7D" },
        { "|", "%7C" },
        { "\\", "%5C" },
        { "^", "%5E" },
        { "~", "%7E" },
        { "[", "%5B" },
        { "]", "%5D" },
        { "`", "%60" } };

        public static string EncodeUrl(string url)
        {
            for (int i = 0; i < _chars.GetUpperBound(0); i++)
                url = url.Replace(_chars[i, 0], _chars[i, 1]);

            return url;
        }

        public static string DecodeUrl(string url)
        {
            for (int i = 0; i < _chars.GetUpperBound(0); i++)
                url = url.Replace(_chars[i, 1], _chars[i, 0]);

            return url;
        }
    }
}
