using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterMonitor.Models
{
    public interface ITweetRepository
    {
        IQueryable<Tweet> GetAllTweets();
        Tweet GetTweet(string id);
        Tweet AddTweet(Tweet item);
        bool RemoveTweet(string id);
        bool UpdateTweet(string id, Tweet item);        
        IQueryable<Tweet> MonitorTweetsByText(string text);
    }
}