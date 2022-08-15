namespace NapierUnitTests
{
    [TestClass]
    public class TweetTests
    {
        [TestMethod]
        public void VerifyCollateMentionsWorks()
        {
            //Arrange
            string tweetBody = "@Tom I think that #NapierBank is a great bank LOL";
            string tweetHeader = "T123456789";
            char tweetType = 'T';
            string tweetSender = "@Tom";
            Dictionary<string, string> abbrevations = new Dictionary<string, string>();
            abbrevations.Add("LOL", "Laughing out Loud");
            
            Hashtag tag = new Hashtag("#Tag");
            List<Hashtag> hashtags = new List<Hashtag>();
            hashtags.Add(tag);

            Mention mention = new Mention("@Tom");
            List<Mention> mentions = new List<Mention>();
            mentions.Add(mention);

            Tweet tweet = new Tweet(tweetBody, tweetHeader, tweetType, tweetSender, hashtags, mentions);
            List<Tweet> tweetList = new List<Tweet>();
            tweetList.Add(tweet);

            List<Mention> IDs = new List<Mention>();

            //Act
            List<string> expectedResult = new List<string>();
            List<string> actualResult = Tweet.CollateMentions(tweetList);

            foreach (Tweet t in tweetList)
            {
                IDs.Add(t.Mentions.ElementAt(0));
            }

            foreach (Mention id in IDs)
            {
                expectedResult.Add(id.UserID);
            }

            //Assert
            Assert.AreEqual(expectedResult.First(), actualResult.First());
        }

        [TestMethod]
        public void VerifyCollateHashtagsWorks()
        {
            //Arrange
            string tweetBody = "@Tom I think that #NapierBank is a great bank LOL";
            string tweetHeader = "T123456789";
            char tweetType = 'T';
            string tweetSender = "@Tom";
            Dictionary<string, string> abbrevations = new Dictionary<string, string>();
            abbrevations.Add("LOL", "Laughing out Loud");

            Hashtag tag = new Hashtag("#NapierBank");
            List<Hashtag> hashtags = new List<Hashtag>();
            hashtags.Add(tag);

            Mention mention = new Mention("@Tom");
            List<Mention> mentions = new List<Mention>();
            mentions.Add(mention);

            Tweet tweet = new Tweet(tweetBody, tweetHeader, tweetType, tweetSender, hashtags, mentions);
            List<Tweet> tweetList = new List<Tweet>();
            tweetList.Add(tweet);


            //Act
            Dictionary<string, int> expectedResult = new Dictionary<string, int>();
            expectedResult.Add("#NapierBank", 1);
            Dictionary<string, int> actualResult = Tweet.CollateHashtags(tweetList);

            //Assert
            Assert.AreEqual(expectedResult.ElementAt(0), actualResult.ElementAt(0));
            Assert.IsNotNull(actualResult);
        }


        [TestMethod]
        public void VerifyDetectTweetSenderWorks()
        {
            //Arrange
            Tweet tweet = new Tweet();
            string body = "@Tom I'm super happy that #Napier bank is around...";

            //Act
            string expectedResult = "@Tom";
            string actualResult = tweet.DetectTweetSender(body);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void VerifyDetectMentionsWorks()
        {
            //Arrange
            Tweet tweet = new Tweet();
            string body = "@Tom I'm super happy that @Greg from #Napier bank is around...";


            //Act
            string expectedResult = "@Greg";
            List<Mention> resultList = tweet.DetectMentions(body);
            Mention actualResultMention = resultList.ElementAt(1);
            string? actualResult = actualResultMention.UserID;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod] 
        public void VerifyDetectHashtagsWorks()
        {
            //Arrange
            Tweet tweet = new Tweet();
            string body = "@Tom I'm super happy that @Greg from #Napier bank is around...";

            //Act
            string expectedResult = "#Napier";
            List<Hashtag> resultList = tweet.DetectHashtags(body);
            Hashtag actualResultHashtag = resultList.ElementAt(0);
            string? actualResult = actualResultHashtag.Tag;

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void VerifyProcessTweetWorks()
        {
            //Arrange
            Tweet tweet = new Tweet();
            string body = "@Tom I'm super happy that @Greg from #Napier bank is around LOL...";
            string header = "T123456789";
            char type = 'T';
            Dictionary<string, string> abbrevations = new Dictionary<string, string>();
            abbrevations.Add("LOL", "Laughing out Loud");


            //Act
            string expectedBody = "@Tom I'm super happy that @Greg from #Napier bank is around LOL <Laughing out Loud>...";
            string expectedHeader = "T123456789";
            char exptectedType = 'T';
            string expectedSender = "@Tom";

            Hashtag tag = new Hashtag("#Napier");
            List<Hashtag> hashtags = new List<Hashtag>();
            hashtags.Add(tag);

            Mention mention = new Mention("@Tom");
            List<Mention> mentions = new List<Mention>();
            mentions.Add(mention);

            Tweet expectedTweet = new Tweet(expectedHeader, expectedBody, exptectedType, expectedSender, hashtags, mentions);
            Tweet actualTweet = Tweet.ProcessTweet(body, header, type, abbrevations);

            Hashtag actualHashtag = actualTweet.HashTags.First();
            string? actualHashtagString = actualHashtag.Tag;

            Mention actualMention = actualTweet.Mentions.First();
            string? actualMentionString = actualMention.UserID;

            //Assert
            Assert.AreEqual(expectedTweet.MessageHeader, actualTweet.MessageHeader);
            Assert.AreEqual(expectedTweet.MessageBody, actualTweet.MessageBody);
            Assert.AreEqual(expectedTweet.Sender, actualTweet.Sender);
            Assert.AreEqual(tag.Tag, actualHashtagString);
            Assert.AreEqual(mention.UserID, actualMentionString);

        }


    }
}