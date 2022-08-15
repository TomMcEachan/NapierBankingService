namespace NapierUnitTests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void VerifyDetectDateWorks()
        {
            //Arrange
            Email email = new Email();
            string date = "10/09/1997";
            
            //Act
            string expectedResult = "10/09/1997";
            string actualResult = email.DetectDate(date);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);          
        }


        [TestMethod] 
        public void VerifyDetectEmailWorks()
        {
            //Arrange
            Email email = new Email();
            string mail = "tom.a.mceachan@outlook.com";
           

            //Act
            string expectedResult = "tom.a.mceachan@outlook.com";
            string actualResult = email.DetectEmailAddress(mail);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void VerifyDetectURLWorks()
        {
            //Arrange
            Email email = new Email();
            List<string> urls = new List<string>();     
            string url = "https://www.parliament.scot";
            urls.Add(url);

            //Act
            List<string> expectedResult = urls;
            List<string> actualResult = email.DetectURL(url);

            //Assert
            Assert.AreEqual(expectedResult.ElementAt(0), actualResult.ElementAt(0));

        }


        [TestMethod] 
        public void VerifyQuarantineURLWorks()
        {
            //Arrange
            Email email = new Email();
            string url = "https://www.parliament.scot";
            string body = "The body contains the https://www.parliament.scot url";
            List<string> URLs = new List<string>();
            URLs.Add(url);

            //Act
            string expectedResult = "The body contains the <URL Quarantined> url";
            string actualResult = email.QuarantineURL(body, URLs);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }


        [TestMethod] 
        public void VerifyDetectSubjectLineWorks()
        {
            //Arrange
            Email email = new Email();
            string subject = "Subject: this is a subject line Body: this is the body text";

            //Act
            string expectedResult = "Subject: this is a subject line ";
            string actualResult = email.DetectSubjectLine(subject);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void VerifyDetectBodyTextWorks()
        {
            //Arrange
            Email email = new Email();
            string body = "Body: this is some body text";

            //Act
            string expectedResult = "Body: this is some body text";
            string actualResult = email.DetectBodyText(body);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void VerifyProcessEmailWorks()
        {
            //Arrange
            string body = "From: tom.a.mceachan@outlook.com Subject: This is a subject line 10/09/1997 Body: This is some body text that includes a url https://www.parliament.scot";
            string header = "E123456789";
            char type = 'E';

            //Act
            string expectedSubject = "Subject: This is a subject line 10/09/1997 ";
            string expectedHeader = "E123456789";
            string expectedBody = "Body: This is some body text that includes a url <URL Quarantined>";
            char expectedType = 'E';
            string expectedSender = "tom.a.mceachan@outlook.com";
            string expectedDate = "10/09/1997";

            Email expectedEmail = new Email(expectedHeader, expectedBody, expectedType, expectedSubject, expectedSender, expectedDate);
            Email actualEmail = Email.ProcessEmail(body, header, type);

            //Assert
            Assert.AreEqual(expectedEmail.Subject, actualEmail.Subject);
            Assert.AreEqual(expectedEmail.Sender, actualEmail.Sender);
            Assert.AreEqual(expectedEmail.MessageHeader, actualEmail.MessageHeader);
            Assert.AreEqual(expectedEmail.MessageBody, actualEmail.MessageBody);
            Assert.AreEqual(expectedEmail.MessageType, actualEmail.MessageType);
            Assert.AreEqual(expectedEmail.DateString, actualEmail.DateString);         
        }








    }
}