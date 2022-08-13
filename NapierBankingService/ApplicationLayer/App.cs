using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace NapierBankingService.ApplicationLayer
{
    public class App
    {
        private Dictionary<string, int> hashtagDict;
        private List<SignificantIncident> SIRList;
        private Dictionary<string, string> abbreviations;
        private List<Tweet> tweetList;
        private char type;
        private int smsTwitterLimit = 140;
        private int emailLimit = 1028;

        public int EmailLimit { get => emailLimit; set => emailLimit = value; }
        public int SmsTwitterLimit { get => smsTwitterLimit; set => smsTwitterLimit = value; }
        public Dictionary<string, string> Abbreviations { get => abbreviations; set => abbreviations = value; }
        public List<Tweet> TweetList { get => tweetList; set => tweetList = value; }
        public Dictionary<string, int> HashtagDict { get => hashtagDict; set => hashtagDict = value; }



        /// <summary>
        /// This method is triggered on application start-up
        /// </summary>
        public void StartUp()
        {
            Abbreviations = DataLayer.LoadData.ReadTextWordsCSV();
            DataLayer.LoadData.DeserializeEmails();
            DataLayer.LoadData.DeserializeSMSs();
            DataLayer.LoadData.DeserializeSignificantIncidents();
            TweetList = DataLayer.LoadData.DeserializeTweets();
            HashtagDict = Tweet.CollateHashtags(TweetList);
        }



        /// <summary>
        /// This method processes the message that is added by the user through the GUI
        /// </summary>
        /// <param name="header"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="abbreviations"></param>
        public void ProcessSubmission(string header, string body, string subject)
        {
            type = ProcessHeader(header);
            ProcessMessage(body, header, subject, type, abbreviations);           
        }

     
        /* Methods for Processing the Header Information */

        /// <summary>
        /// This method processes the header information and returns the correct type
        /// </summary>
        /// <param name="header"></param>
        /// <returns>
        /// A char signifying the type of message recieved 
        /// </returns>
        public char ProcessHeader(string header)
        {
            string headerText = header;
            type = DetectType(header);
            return type;
        }



        /// <summary>
        /// This method checks whether or not the user input is valid
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public bool HeaderValid (string header)
        {
            bool valid = true;

            if (header.Length > 10)
            {
                MessageBox.Show("Header is too long - please input a valid message header", "Error" );
                valid = false;
            }

            if (header.Length < 10)
            {
                MessageBox.Show("Header is not long enough - please input a valid message header", "Error");
                valid = false;
            }

            if (!header.Contains("S") && !header.Contains("E") && !header.Contains("T"))
            {
                MessageBox.Show("Header does not contain a message type - please input a valid message header", "Error");
                valid = false;
            }

            if (header.Contains("s") && header.Contains("e") && header.Contains("t"))
            {
                MessageBox.Show("Header type is not the correct case - please input a valid message header", "Error");
                valid = false;
            }

            return valid;
        }


        
        /// <summary>
        /// This method detects the type of message
        /// </summary>
        /// <param name="headerText"></param>
        /// <returns>
        /// A char signifying the type of message recieved
        /// </returns>
        public char DetectType(string headerText)
        {
            if (headerText.Contains("S"))
            {
                type = 'S';
            }

            if (headerText.Contains("T"))
            {
                type = 'T';
            }

            if (headerText.Contains("E"))
            {
                type = 'E';
            }

            return type;
        }



        /* Methods for Processing Messages */

        /// <summary>
        /// This method processes a message based on the specification of its type
        /// </summary>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="subject"></param>
        /// <param name="type"></param>
        public void ProcessMessage(string body, string header, string subject, char type, Dictionary<string, string> abbreviations)
        {
            bool incidentDetected;
            switch (type)
            {
                case 'S':

                    SMS sms = SMS.ProcessSMS(body, header, type, abbreviations);
                    DataLayer.SaveData.SerializeSMS(sms);

                    break;
                case 'T':

                    Tweet tweet = Tweet.ProcessTweet(body, header, type, abbreviations);
                    DataLayer.SaveData.SerializeTweet(tweet);
                    TweetList = DataLayer.LoadData.DeserializeTweets();

                    break;
                case 'E':
                    
                    incidentDetected = SignificantIncident.DetectIncident(subject); //detects whether or not the email is a significant incident (true or false)
                    
                    if (incidentDetected)
                    {
                        SignificantIncident significantIncident = SignificantIncident.ProcessSignificantIncident(body, subject, header, type);
                        DataLayer.SaveData.SerializeSignificantIncident(significantIncident);
                    }
                    
                    if (!incidentDetected)
                    {
                        Email email = Email.ProcessEmail(body, subject, header, type);
                        DataLayer.SaveData.SerializeEmail(email);
                    }
                    break;
            }
            
        }


        public Tuple<string, string, string> EndSession(Dictionary<string, int> trendingList)
        {
            string hello = "hello";
            string hello2 = "hello";
            var trendingHashtags = trendingList.Select(kvp => string.Format("Hashtag: {0} ---- Times Used: {1}", kvp.Key, kvp.Value, kvp.Value));
            var trending = string.Join(Environment.NewLine, trendingHashtags);


            return new Tuple<string, string,string>(trending, hello, hello2);

        }


        





        

    
    }
}
