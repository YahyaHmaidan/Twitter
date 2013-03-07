using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using TwitterMonitor.UtilityClasses;

namespace TwitterMonitor.Models
{
    public class TweetRepository : ITweetRepository
    {        
        public TweetRepository()
            : this(string.Empty)
        {                        
        }

        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Tweet> _tweets;       
        
        public TweetRepository(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = ConfigurationManager.AppSettings["MongoDBConnectionString"].ToString();
            }           

            var client = new MongoClient(connection);
            
            _server = client.GetServer();
            _database = _server.GetDatabase(ConfigurationManager.AppSettings["TwitterMonitorDatabaseName"].ToString());
            _tweets = _database.GetCollection<Tweet>(ConfigurationManager.AppSettings["TweetDocumentName"].ToString());            
        }        

        public IQueryable<Tweet> GetAllTweets()
        {            
            return _tweets.FindAll().AsQueryable();
        }

        public Tweet GetTweet(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            return _tweets.Find(query).FirstOrDefault();
        }

        public Tweet AddTweet(Tweet item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            _tweets.Insert(item);
            return item;
        }

        public bool RemoveTweet(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            WriteConcernResult result = _tweets.Remove(query);
            return result.DocumentsAffected == 1;
        }

        public bool UpdateTweet(string id, Tweet tweet)
        {
            IMongoQuery query = Query.EQ("_id", id);
            IMongoUpdate update = Update
                .Set("Text", tweet.Text)
                .Set("CreatedAt", tweet.CreatedAt);
            WriteConcernResult result = _tweets.Update(query, update);
            return result.UpdatedExisting;            
        }

        public IQueryable<Tweet> MonitorTweetsByText(string text)
        {
            text = UrlEncoder.EncodeUrl(text);    

            List<Parameter> parameters = new List<Parameter> { new Parameter { Name = "q", Value = text, Type = ParameterType.GetOrPost } };
            IRestResponse tweetsJson = ResponseFetcher.GetJsonOrAtomResponse(
                                                       ConfigurationManager.AppSettings["TwitterSearchApiBaseUrl"].ToString(),
                                                       ConfigurationManager.AppSettings["TwitterSearchApiResource"].ToString(),
                                                       RestSharp.Method.GET, parameters);



            tweetsJson.Content = JObject.Parse(tweetsJson.Content).Property("results").FirstOrDefault().ToString();

            var tweetsArray = JArray.Parse(tweetsJson.Content);
            var tweets = from tweet in tweetsArray
                         select new Tweet
                         {
                             Text = tweet["text"].ToString(),
                             CreatedAt = DateTime.Parse(tweet["created_at"].ToString()),
                             Id = tweet["id"].ToString(),
                             User = new TwitterUser
                                      {
                                          Name = tweet["from_user_name"].ToString(),
                                          Id = tweet["from_user_id"].ToString(),
                                          ScreenName = tweet["from_user"].ToString(),
                                          Location = tweet["geo"].ToString(),
                                          ProfileImage = tweet["profile_image_url"].ToString()
                                      }
                         };
            
            //Write on Mongo Database:
            _tweets.InsertBatch<Tweet>(tweets);

            return tweets.ToList<Tweet>().AsQueryable();           
        }       
    }
}