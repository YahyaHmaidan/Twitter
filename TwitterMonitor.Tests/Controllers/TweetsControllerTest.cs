using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterMonitor;
using TwitterMonitor.Controllers;
using TwitterMonitor.Models;

namespace TwitterMonitor.Tests.Controllers
{
    [TestClass]
    public class TweetsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            TweetsController controller = new TweetsController();

            // Act
            IQueryable<Tweet> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);            
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange    
            TweetsController controller = new TweetsController();

            // Act
            Tweet result = controller.GetTweet("123");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MonitorTweetsByText()
        {
            // Arrange           
            TweetsController controller = new TweetsController();

            // Act
            IEnumerable<Tweet> result = controller.MonitorTweetsByText("jobs");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual<int>(0, result.Count<Tweet>());
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            TweetsController controller = new TweetsController();

            // Act
            Tweet tweet = new Tweet();
            tweet.CreatedAt = DateTime.UtcNow;
            tweet.Text = "test text";
            tweet.User = new TwitterUser();
            tweet.User.Name = "test user";
            tweet.User.ScreenName = "testUser";
            tweet = controller.Post(tweet);

            // Assert
            Assert.IsNotNull(tweet);
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            TweetsController controller = new TweetsController();

            // Act
            Tweet tweet = new Tweet();
            tweet.CreatedAt = DateTime.UtcNow;
            tweet.Text = "test text";
            tweet.User = new TwitterUser();
            tweet.User.Name = "test user";
            tweet.User.ScreenName = "testUser";
            HttpResponseMessage msg = controller.Put("123", tweet);

            // Assert
            Assert.AreEqual(msg.StatusCode, HttpStatusCode.Accepted);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            TweetsController controller = new TweetsController();

            // Act
            HttpResponseMessage msg = controller.Delete("123");

            // Assert
            Assert.AreEqual(msg.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void GetTweetsByAuthorName()
        {
            // Arrange
            TweetsController controller = new TweetsController();

            // Act
            IQueryable<Tweet> tweets = controller.GetTweetsByAutherName("test user");

            // Assert
            Assert.AreNotEqual(tweets.Count(),0);
        }

        [TestMethod]
        public void GetTweetsByDate()
        {
            // Arrange
            TweetsController controller = new TweetsController();
            DateTime fromDate = new DateTime(2013,1,1,0,0,0,0), toDate = DateTime.Now;

            // Act
            IQueryable<Tweet> tweets = controller.GetTweetsByDate(fromDate,toDate);

            // Assert
            Assert.AreNotEqual(tweets.Count(), 0);
        }

        [TestMethod]
        public void GetTweetsByTextLength()
        {
            // Arrange
            TweetsController controller = new TweetsController();
            int textLength = 50;

            // Act
            IQueryable<Tweet> tweets = controller.GetTweetsByTextLength(textLength);

            // Assert
            Assert.AreNotEqual(tweets.Count(), 0);
        }
    }
}
