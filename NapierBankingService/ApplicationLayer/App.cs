using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace NapierBankingService.ApplicationLayer
{
    public class App
    {
        private Dictionary<string, int>? hashtagDict;
        private List<SignificantIncident>? sirList;
        private Dictionary<string, string>? sirStringList;
        private List<string>? mentionsList;
        private Dictionary<string, string>? abbreviations;
        private List<Tweet>? tweetList;
        private char type;
        private int smsTwitterLimit = 140;
        private int emailLimit = 1028;

        public int EmailLimit { get => emailLimit; set => emailLimit = value; }
        public int SmsTwitterLimit { get => smsTwitterLimit; set => smsTwitterLimit = value; }
        public Dictionary<string, string>? Abbreviations { get => abbreviations; set => abbreviations = value; }
        public List<Tweet>? TweetList { get => tweetList; set => tweetList = value; }
        public Dictionary<string, int>? HashtagDict { get => hashtagDict; set => hashtagDict = value; }
        public List<string>? MentionsList { get => mentionsList; set => mentionsList = value; }
        public List<SignificantIncident>? SIRList { get => sirList; set => sirList = value; }
        public Dictionary<string, string>? SirStringList { get => sirStringList; set => sirStringList = value; }



        /// <summary>
        /// This method is triggered on application start-up
        /// </summary>
        public bool StartUp()
        {
            try
            {
                Abbreviations = DataLayer.LoadData.ReadTextWordsCSV();
                DataLayer.LoadData.DeserializeEmails();
                DataLayer.LoadData.DeserializeSMSs();
                SIRList = DataLayer.LoadData.DeserializeSignificantIncidents();
                TweetList = DataLayer.LoadData.DeserializeTweets();
                HashtagDict = Tweet.CollateHashtags(TweetList);
                MentionsList = Tweet.CollateMentions(TweetList);
                SirStringList = SignificantIncident.CollateSignificantIncidents(SIRList);

                return true;
            } 
            catch
            {
                return false;
            }       
        }



        /// <summary>
        /// This method processes the message that is added by the user through the GUI
        /// </summary>
        /// <param name="header"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="abbreviations"></param>
        public char ProcessSubmission(string header, string body)
        {
            
            type = ProcessHeader(header);
            ProcessMessage(body, header, type, abbreviations);    
            return type;
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
                valid = false;
            }
            if (header.Length < 10)
            {  
                valid = false;
            }
            if (!header.Contains("S") && !header.Contains("E") && !header.Contains("T"))
            {  
                valid = false;
            }
            if (header.Contains("s") && header.Contains("e") && header.Contains("t"))
            {     
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
        public bool ProcessMessage(string body, string header, char type, Dictionary<string, string> abbreviations)
        {
            bool incidentDetected;
            bool created = false;
            switch (type)
            {
                case 'S':

                    try
                    {
                        CreateSMS(body, header, type, abbreviations);
                        created = true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                        created = false;
                    }              
                    break;
                case 'T':
                    try
                    {
                        CreateTweet(body, header, type, abbreviations);
                        created = true;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                        created= false;
                    }
                  
                    break;
                case 'E':
                    
                    incidentDetected = SignificantIncident.DetectIncident(body); //detects whether or not the email is a significant incident (true or false)
                    
                    if (incidentDetected)
                    {
                        try
                        {
                            CreateSigIncident(body, header, type);
                            created = true;
                        } 
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            created=false;
                        }
                       
                    }
                    
                    if (!incidentDetected)
                    {
                        try
                        {
                            CreateEmail(body, header, type);
                            created=true;
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            created = false;
                        }
                    }
                    break;
            }

            return created;
        }



        /// <summary>
        /// This method creates an SMS
        /// </summary>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <param name="abbreviations"></param>
        /// <returns>
        /// A complete SMS Message
        /// </returns>
        public SMS CreateSMS (string body, string header, char type, Dictionary<string, string> abbreviations)
        {
            SMS sms = SMS.ProcessSMS(body, header, type, abbreviations);
            DataLayer.SaveData.SerializeSMS(sms);
            DataLayer.LoadData.DeserializeSMSs();

            return sms;
        }

        /// <summary>
        /// This method creates a Tweet Message
        /// </summary>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <param name="abbreviations"></param>
        /// <returns>
        /// A complete Tweet
        /// </returns>
        public Tweet CreateTweet (string body, string header, char type, Dictionary<string, string> abbreviations)
        {
            Tweet tweet = Tweet.ProcessTweet(body, header, type, abbreviations);
            DataLayer.SaveData.SerializeTweet(tweet);
            TweetList = DataLayer.LoadData.DeserializeTweets();
            HashtagDict = Tweet.CollateHashtags(TweetList);
            MentionsList = Tweet.CollateMentions(TweetList);

            return tweet;
        }


        /// <summary>
        /// This method creates a Significant Incident
        /// </summary>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <returns>
        /// A complete Significant Incident Message
        /// </returns>
        public SignificantIncident CreateSigIncident(string body, string header, char type)      
        {
            SignificantIncident significantIncident = SignificantIncident.ProcessSignificantIncident(body, header, type);
            DataLayer.SaveData.SerializeSignificantIncident(significantIncident);
            SIRList = DataLayer.LoadData.DeserializeSignificantIncidents();
            SirStringList = SignificantIncident.CollateSignificantIncidents(SIRList);

            return significantIncident;
        }


        /// <summary>
        /// This methoid creates an Email
        /// </summary>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <returns>
        /// A complete Email Message
        /// </returns>
        public Email CreateEmail (string body, string header, char type)
        {

            Email email = Email.ProcessEmail(body, header, type);
            DataLayer.SaveData.SerializeEmail(email);

            return email;
        }




        public Tuple<string, List<string>, string> EndSession(Dictionary<string, int> trendingList, List<string> mentions, Dictionary<string, string> sigs)
        {
             
            //Creates a string to be printed in the end of session window for the trending hashtags
            if(trendingList != null)
            {
                var trendingHashtags = trendingList.Select(kvp => string.Format("Hashtag: {0} ---- Times Used: {1}", kvp.Key, kvp.Value, kvp.Value));
                var trending = string.Join(Environment.NewLine, trendingHashtags);

                var sigIncidents = sigs.Select(kvp => string.Format("Sort Code: {0} ---- Incident Type: {1}", kvp.Key, kvp.Value, kvp.Value));
                var sigList = string.Join(Environment.NewLine, sigIncidents);
                return new Tuple<string, List<string>, string>(trending, mentions, sigList);
            }


            return new Tuple<string, List<string>, string>("empty", mentions, "empty");

        }


        





        

    
    }
}
