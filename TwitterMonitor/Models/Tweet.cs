using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace TwitterMonitor.Models
{
    [Serializable, XmlRoot("status")]    
    public class Tweet
    {
        
        [XmlElementAttribute("created_at")]
        public DateTime CreatedAt { get; set; }        
        [XmlElementAttribute("id")]
        [BsonId]
        public string Id { get; set; }        
        [XmlElementAttribute("text")]
        public string Text { get; set; }
        [XmlElement("Users")]
        public TwitterUser User { get; set; }

        internal static Tweet Deserialize(XmlNode tweetXml)
        {
            var reader = new StringReader(tweetXml.OuterXml);
            var serializer = new XmlSerializer(typeof(Tweet));
            var tweet = (Tweet)serializer.Deserialize(reader);
            return tweet;                        
        }
    }   
}