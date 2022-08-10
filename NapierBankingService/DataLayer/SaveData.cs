using Newtonsoft.Json;
using System;
using System.IO;

namespace NapierBankingService.DataLayer
{
    public class SaveData
    {

       
        public SaveData() { }

        
        public static void SerializeTweet(ApplicationLayer.Tweet tweet)
        {       
            string fileName = tweet.MessageHeader + ".json";
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\Tweets";

            System.IO.Directory.CreateDirectory(location);
            
            string pathString = System.IO.Path.Combine(location, fileName);

            string output = JsonConvert.SerializeObject(tweet);
            File.WriteAllText(pathString, output);
        }

        public static void SerializeEmail(ApplicationLayer.Email email)
        {
            string fileName = email.MessageHeader + ".json";
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\Emails";

            System.IO.Directory.CreateDirectory(location);

            string pathString = System.IO.Path.Combine(location, fileName);

            string output = JsonConvert.SerializeObject(email);
            File.WriteAllText(pathString, output);
        }

        public static void SerializeSignificantIncident(ApplicationLayer.SignificantIncident sig)
        {
            string fileName = sig.MessageHeader + ".json";
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\Significant-Incidents";

            System.IO.Directory.CreateDirectory(location);

            string pathString = System.IO.Path.Combine(location, fileName);

            string output = JsonConvert.SerializeObject(sig);
            File.WriteAllText(pathString, output);
        }

        public static void SerializeSMS(ApplicationLayer.SMS sms)
        {
            string fileName = sms.MessageHeader + ".json";
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\SMS";

            System.IO.Directory.CreateDirectory(location);

            string pathString = System.IO.Path.Combine(location, fileName);

            string output = JsonConvert.SerializeObject(sms);
            File.WriteAllText(pathString, output);
        }


    }
}
