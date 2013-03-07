using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TwitterMonitor.Models
{
    [Serializable, XmlRoot("Users")]    
    public class TwitterUser
    {        
        [XmlElementAttribute("from_user_id")]        
        public string Id { get; set; }
        [XmlElementAttribute("from_user_name")]              
        public string Name { get; set; }
        [XmlElementAttribute("from_user")]               
        public string ScreenName { get; set; }
        [XmlElementAttribute("geo")]        
        public string Location { get; set; }
        [XmlElementAttribute("description")]        
        public string Description { get; set; }
        [XmlElementAttribute("profile_image_url")]        
        public string ProfileImage { get; set; }
        [XmlElementAttribute("url")]        
        public string Url { get; set; }
        [XmlElementAttribute("protected")]        
        public bool IsProtected { get; set; }
        [XmlElementAttribute("followers_count")]        
        public long FollowersCount { get; set; }
        [XmlElementAttribute("friends_count")]        
        public long FriendsCount { get; set; }
        [XmlElementAttribute("created_at")]        
        public string CreatedAt { get; set; }
        [XmlElementAttribute("favourites_count")]        
        public long FavoritesCount { get; set; }
        [XmlElementAttribute("verified")]        
        public bool Verified { get; set; }
        [XmlElementAttribute("statuses_count")]        
        public long StatusCount { get; set; }
    }
}