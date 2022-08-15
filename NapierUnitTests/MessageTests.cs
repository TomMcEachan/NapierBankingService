namespace NapierUnitTests
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void VerifyExpandTextSpeakWorksAsExpected()
        {
            Dictionary<string, string> messages = new Dictionary<string, string>();

            messages.Add("ASAP", "As soon as Possible");
            string body = "ASAP";
           

            string expectedResult = "ASAP <As soon as Possible>";
            string actualResult = Message.ExpandTextSpeak(body, messages);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}