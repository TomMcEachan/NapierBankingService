namespace NapierUnitTests
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void VerifyExpandTextSpeakWorksAsExpected()
        {
            //Arrange
            Dictionary<string, string> messages = new Dictionary<string, string>();
            messages.Add("ASAP", "As soon as Possible");
            string body = "ASAP";
           
            //Act
            string expectedResult = "ASAP <As soon as Possible>";
            string actualResult = Message.ExpandTextSpeak(body, messages);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}