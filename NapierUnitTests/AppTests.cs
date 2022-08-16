namespace NapierUnitTests
{
    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void VerifyStartUpWorks()
        {
            //Arrange
            NapierBankingService.ApplicationLayer.App app = new NapierBankingService.ApplicationLayer.App();
            bool complete;

            //Act
            complete = app.StartUp();

            //Assert
            Assert.IsTrue(complete);
        }

        [TestMethod]
        public void VerifyProcessSubmission()
        {

            //Arrange
            NapierBankingService.ApplicationLayer.App app = new NapierBankingService.ApplicationLayer.App();
            string header = "S123456789";
            string body = "+447402273303 hello there ASAP";
            app.StartUp();


            //Act
            char expectedType = 'S';
            char actualType = app.ProcessSubmission(header, body);

            //Assert
            Assert.AreEqual(expectedType, actualType);

        }

        [TestMethod] 
        public void VeryifyProcessHeaderWorks()
        {
            //Arrange
            NapierBankingService.ApplicationLayer.App app = new NapierBankingService.ApplicationLayer.App();
            string header = "E123456789";

            //Act
            char expectedResult = 'E';
            char actualResult = app.ProcessHeader(header);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void VerifyHeaderValidWorks()
        {
            //Arrange
            NapierBankingService.ApplicationLayer.App app = new NapierBankingService.ApplicationLayer.App();
            string headerTooLong = "E1234567890";
            string headerTooShort = "E1";
            string headerNoType = "1234";
            string headerTypeLowerCase = "e";
            string headerValid = "E123456789";

            //Act
            bool actualTooLong = app.HeaderValid(headerTooLong);
            bool actualTooShort = app.HeaderValid(headerTooShort);
            bool actualNoType = app.HeaderValid(headerNoType);
            bool actualLowerCase = app.HeaderValid(headerTypeLowerCase);
            bool actualValid = app.HeaderValid(headerValid);

            //Assert
            Assert.IsFalse(actualTooLong);
            Assert.IsFalse(actualTooShort);
            Assert.IsFalse(actualNoType);
            Assert.IsFalse(actualLowerCase);
            Assert.IsTrue(actualValid);
        }

        [TestMethod]
        public void VerifyDetectTypeWorks()
        {
            //Arrange
            NapierBankingService.ApplicationLayer.App app = new NapierBankingService.ApplicationLayer.App();
            string headerSMS = "S123456789";
            string headerEmail = "E123456789";
            string headerTweet = "T123456789";

            //Act
            char expectedTweet = 'T';
            char expectedSMS = 'S';
            char expectedEmail = 'E';

            char actualTweet = app.DetectType(headerTweet);
            char actualEmail = app.DetectType(headerEmail);
            char actualSMS = app.DetectType(headerSMS);

            //Assert
            Assert.AreEqual(expectedSMS, actualSMS);
            Assert.AreEqual(expectedTweet, actualTweet);
            Assert.AreEqual(expectedEmail, actualEmail);
        }


        [TestMethod] 
        public void VerifyProcessMessageWorks()
        {
            //Arrange 
            string SMSbody = "+447402273303 hello how are you? I need you to speak to me ASAP";
            string SMSheader = "S123456789";
            char SMStype = 'S';

            string TweetBody = "@Tom hello there my good friend #NapierBank";
            string TweetHeader = "T123456789";
            char TweetType = 'T';

            string SigBody = "From: tom.a.mceachan@outlook.com Subject: SIR Theft 00-00-00Body:Hello there my good friend";
            string SigHeader = "E123456789";
            char SigType = 'E';

            string EmailBody = "From: tom.a.mceachan@outlook.com Subject: hello friendBody:Hello there my good friend";
            string EmailHeader = "E123456789";
            char EmailType = 'E';

            NapierBankingService.ApplicationLayer.App app = new NapierBankingService.ApplicationLayer.App();
            app.StartUp();

            //Act
            bool expectedSMS = app.ProcessMessage(SMSbody, SMSheader, SMStype, app.Abbreviations);
            bool expectedSig = app.ProcessMessage(SigBody, SigHeader, SigType, app.Abbreviations);
            bool expectedTweet = app.ProcessMessage(TweetBody, TweetHeader, TweetType, app.Abbreviations);
            bool expectedEmail = app.ProcessMessage(EmailBody, EmailHeader, EmailType, app.Abbreviations);
        
            //Assert
            Assert.IsTrue(expectedSMS);
            Assert.IsTrue(expectedTweet);
            Assert.IsTrue(expectedSig);
            Assert.IsTrue(expectedEmail);
        }

        [TestMethod] 
        public void VerifyEndSessionWorks()
        {
            //Arrange
            NapierBankingService.ApplicationLayer.App app = new NapierBankingService.ApplicationLayer.App();
            Dictionary<string, int> trendingList = new Dictionary<string, int>();
            List<string> mentions = new List<string>();
            Dictionary<string, string> sigs = new Dictionary<string, string>();

            trendingList.Add("#Napier", 10);
            mentions.Add("@Tom");
            sigs.Add("00-00-00", "Theft");

            var trendingHashtags = trendingList.Select(kvp => string.Format("Hashtag: {0} ---- Times Used: {1}", kvp.Key, kvp.Value, kvp.Value));
            var trending = string.Join(Environment.NewLine, trendingHashtags);

            var sigIncidents = sigs.Select(kvp => string.Format("Sort Code: {0} ---- Incident Type: {1}", kvp.Key, kvp.Value, kvp.Value));
            var sigList = string.Join(Environment.NewLine, sigIncidents);

            //Act
            Tuple<string, List<string>, string> expected = new Tuple<string, List<string>, string>(trending, mentions, sigList);
            Tuple<string, List<string>, string> actual = app.EndSession(trendingList, mentions, sigs);

            string actualItem1 = actual.Item1;
            List<string> actualitem2 = actual.Item2;
            string actualitem2Value = actualitem2.ElementAt(0);
            string actualItem3 = actual.Item3;

            string expectedItem1 = expected.Item1;
            List <string> expectedItem2 = expected.Item2;
            string expectedItem2Value = expected.Item2.ElementAt(0);
            string expectedItem3 = expected.Item3;

            //Assert
            Assert.AreEqual(expectedItem1, actualItem1);
            Assert.AreEqual(expectedItem2Value, actualitem2Value);
            Assert.AreEqual(expectedItem3, actualItem3);

        }



    }
}