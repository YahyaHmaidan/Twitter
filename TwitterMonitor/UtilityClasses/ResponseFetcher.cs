using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using RestSharp;
using RestSharp.Deserializers;

namespace TwitterMonitor.UtilityClasses
{
    public class ResponseFetcher
    {
        internal static XmlDocument GetXmlResponse(string uri)
        {
            WebRequest request = WebRequest.Create(new Uri(uri));
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            return document;
        }

        internal static IRestResponse GetJsonOrAtomResponse(string baseUrl,string resource, Method method, List<Parameter> parameters)
        {
            RestClient client = new RestClient(baseUrl);
            var request = new RestRequest(method);
            request.Resource = resource;
            request.Parameters.AddRange(parameters);
            var response = client.Execute(request);
            return response;
        }        
    }
}