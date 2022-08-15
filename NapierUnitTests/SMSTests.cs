namespace NapierUnitTests
{
    [TestClass]
    public class SMSTests
    {
        [TestMethod]
        public void VerifyDetectPhoneNumber()
        {
            //Arrange
            SMS sms = new SMS();
            string body = "+447402273303 hello how are you? I need you to speak to me ASAP <As soon as possible>";

            //Act
            string exptectedResult = "+447402273303";
            string actualReault = sms.DetectPhoneNumber(body);

            //Assert
            Assert.AreEqual(exptectedResult, actualReault);
        }

        [TestMethod] 
        public void VerifyProcessSMSWorks()
        {
            //Arrange
            string body = "+447402273303 hello how are you? I need you to speak to me ASAP";
            string header = "S123456789";
            char type = 'S';
            Dictionary<string, string> abbreviations = new Dictionary<string, string>();
            abbreviations.Add("ASAP", "As soon as possible");

            //Act
            string expectedBody = "+447402273303 hello how are you? I need you to speak to me ASAP <As soon as possible>";
            string expectedHeader = "S123456789";
            char expectedType = 'S';
            string expectedSender = "+447402273303";


            SMS expectedSMS = new SMS(expectedHeader, expectedBody, expectedType, expectedSender);
            SMS actualSMS = SMS.ProcessSMS(body, header, type, abbreviations);
            
            //Assert
            Assert.AreEqual(expectedSMS.Sender, actualSMS.Sender);
            Assert.AreEqual(expectedSMS.MessageType, actualSMS.MessageType);
            Assert.AreEqual(expectedSMS.MessageHeader, actualSMS.MessageHeader);
            Assert.AreEqual(expectedSMS.MessageBody, actualSMS.MessageBody);
        }
    }
}