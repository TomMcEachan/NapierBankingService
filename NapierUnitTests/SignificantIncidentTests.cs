using System.Diagnostics;

namespace NapierUnitTests
{
    [TestClass]
    public class SignificantIncidentTests
    {
        [TestMethod]
        public void VerifyDetectIncidentWorks()
        {
            //Arrange
            string subject = "SIR Theft 00-00-00";
            
            //Act      
            bool Result = SignificantIncident.DetectIncident(subject);

            //Assert
            Assert.IsTrue(Result);          
        }


        [TestMethod]
        public void VerifyCollateSignificantIncidentsWorks()
        {
            //Arrange
            List<SignificantIncident> sigs = new List<SignificantIncident>();
            SignificantIncident sig1 = new SignificantIncident("00-00-00", "Theft", "E123456789", "Body: this is a message body", 'E', "tom@email.com", "Subject: SIR Theft 00-00-00", "10/09/1997");
            SignificantIncident sig2 = new SignificantIncident("00-60-00", "Bomb", "E123456781", "Body: this is a message body", 'E', "tom@anotherEmail.com", "Subject: SIR Bomb 00-60-00", "10/09/1998");

            sigs.Add(sig1);
            sigs.Add(sig2);
            

            //Act
            Dictionary<string, string> actualResult = SignificantIncident.CollateSignificantIncidents(sigs);
            Dictionary<string, string> expectedResult = new Dictionary<string, string>();

            foreach (SignificantIncident sig in sigs)
            {
                expectedResult.Add(sig.SortCode, sig.IncidentType);
            }

            string actualOutput = "actual";
            foreach (KeyValuePair<string, string> kvp in actualResult)
            {
               actualOutput = kvp.Value + " " + kvp.Key;
            }

            string expectedOutput = "expected";
            foreach (KeyValuePair<string, string> kvp in expectedResult)
            {
                expectedOutput = kvp.Value + " " + kvp.Key;
            }

            //Assert
            Assert.AreEqual(actualOutput, expectedOutput);
        }


        [TestMethod] 
        public void VerifyProcessSignificantIncidentWorks()
        {
            //Arrange
            string body = "From: tom.a.mceachan@outlook.com Subject: SIR Theft 00-00-00 10/09/1997 Body: This is the body text https://www.parliament.scot";
            string header = "E123456789";
            char type = 'E';

            
            //Act
            SignificantIncident expectedIncident = new SignificantIncident("00-00-00",
                                                                            "Theft", 
                                                                            "E123456789", 
                                                                            "Body: This is the body text <URL Quarantined>", 
                                                                            'E', 
                                                                            "tom.a.mceachan@outlook.com", 
                                                                            "Subject: SIR Theft 00-00-00 10/09/1997 ", 
                                                                            "10/09/1997");

            SignificantIncident actualIncident = SignificantIncident.ProcessSignificantIncident(body, header, type);

            //Assert
            Assert.AreEqual(expectedIncident.SortCode, actualIncident.SortCode);
            Assert.AreEqual(expectedIncident.Subject, actualIncident.Subject);
            Assert.AreEqual(expectedIncident.IncidentType, actualIncident.IncidentType);
            Assert.AreEqual(expectedIncident.MessageBody, actualIncident.MessageBody);
            Assert.AreEqual(expectedIncident.MessageType, actualIncident.MessageType);
            Assert.AreEqual(expectedIncident.Sender, actualIncident.Sender);
            Assert.AreEqual(expectedIncident.Subject, actualIncident.Subject);
            Assert.AreEqual(expectedIncident.DateString, actualIncident.DateString);
        }

        [TestMethod]
        public void VerifyDetectSortCodeWorks()
        {
            //Arrange
            SignificantIncident sig = new SignificantIncident();
            string subject = "SIR Theft 00-00-00";

            //Act
            string expectedResult = "00-00-00";
            string actualResult = sig.DetectSortCode(subject);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod] 
        public void VerifyDetectIncidentTypeWorks()
        {
            //Arrange
            SignificantIncident sig = new SignificantIncident();
            string subject = "SIR Raid 00-00-00";

            //Act
            string expectedResult = "Raid";
            string actualResult = sig.DetectIncidentType(subject);

            //Assert
            Assert.AreEqual(expectedResult , actualResult);
        }
    }
}