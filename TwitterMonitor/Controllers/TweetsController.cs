using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using TwitterMonitor.Models;
using System.Web.Http;

namespace TwitterMonitor.Controllers
{
    public class TweetsController : ApiController
    {
        static ITweetRepository _tweetsRepository;        
        public TweetsController()
            :this(new TweetRepository())
        {
            _tweetsRepository = new TweetRepository();
        }
        public TweetsController(ITweetRepository repository)
        {
            if (_tweetsRepository == null)
            {                
                repository = new TweetRepository();
            }
            _tweetsRepository = repository;
        }

        public IQueryable<Tweet> Get()
        {
            return _tweetsRepository.GetAllTweets().AsQueryable();
        }

        public Tweet GetTweet(string id)
        {
            Tweet tweet = _tweetsRepository.GetTweet(id);
            if (tweet == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return tweet;
        }        

        public Tweet Post(Tweet tweet)
        {
            tweet = _tweetsRepository.AddTweet(tweet);
            if (tweet == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }
            return tweet;
        }

        public HttpResponseMessage Put(string id, Tweet tweet)
        {
            if (!_tweetsRepository.UpdateTweet(id, tweet))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
        }
        
        public HttpResponseMessage Delete(string id)
        {
            if (_tweetsRepository.RemoveTweet(id))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }        

        [HttpGet]
        public IQueryable<Tweet> MonitorTweetsByText(string text)
        {
            IQueryable<Tweet> tweets = _tweetsRepository.MonitorTweetsByText(text);
            return tweets;            
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public IQueryable<Tweet> GetTweetsByAutherName(string authorName)
        {
            return _tweetsRepository.GetAllTweets().Where(
                p => string.Equals(p.User.Name, authorName, StringComparison.OrdinalIgnoreCase)).AsQueryable();
        }       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IQueryable<Tweet> GetTweetsByDate(DateTime fromDate, DateTime toDate)
        {
            return _tweetsRepository.GetAllTweets().Where(
                tweet => tweet.CreatedAt > fromDate && tweet.CreatedAt < toDate).AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textLength"></param>
        /// <returns></returns>
        public IQueryable<Tweet> GetTweetsByTextLength(int textLength)
        {
            return _tweetsRepository.GetAllTweets().Where(
                tweet => tweet.Text.Length > textLength).AsQueryable();
        }
    }
}
