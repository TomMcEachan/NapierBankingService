﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NapierBankingService.DataLayer
{
    public class LoadData
    {
        /// <summary>
        /// This creates a dictionary for the textwords that are needed to be exapnded
        /// </summary>
        /// <returns>
        /// The text words dictionary
        /// </returns>
        public static Dictionary<string, string> ReadTextWordsCSV()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            using (var stream = new StreamReader(@"Assets\textwords.csv"))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (line == null) continue;
                    var words = line.Split(',');
                    dict.Add(words[0], words[1]);
                }
            }

            return dict;
        }

        /// <summary>
        /// This reads json from file ans deserializes it into an Email object for use in the program
        /// </summary>
        /// <returns>
        /// A list of significant incidents
        /// </returns>
        public static List<ApplicationLayer.Email> DeserializeEmails()
        {
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\Emails";
            string contents = "hello";
            bool empty;
            List <ApplicationLayer.Email> emailList = new List<ApplicationLayer.Email>();

            empty = IsDirectoryEmpty(location);

            if (!empty)
            {
                foreach (string file in Directory.EnumerateFiles(location, "*.json"))
                {
                    contents = File.ReadAllText(file);
                    ApplicationLayer.Email deserializedEmail = JsonConvert.DeserializeObject<ApplicationLayer.Email>(contents);
                    emailList.Add(deserializedEmail);
                }
            }

            return emailList;
        }


        /// <summary>
        /// This reads json from file ans deserializes it into a SMS object for use in the program
        /// </summary>
        /// <returns>
        /// A list of significant incidents
        /// </returns>
        public static List<ApplicationLayer.SMS> DeserializeSMSs()
        {
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\SMS";
            string contents;
            bool empty;
            List<ApplicationLayer.SMS> smsList = new List<ApplicationLayer.SMS>();

            empty = IsDirectoryEmpty(location);

            if (!empty)
            {
                foreach (string file in Directory.EnumerateFiles(location, "*.json"))
                {
                    contents = File.ReadAllText(file);
                    ApplicationLayer.SMS deserializedSMS = JsonConvert.DeserializeObject<ApplicationLayer.SMS>(contents);
                    smsList.Add(deserializedSMS);
                }               
            }

            return smsList;
        }


        /// <summary>
        /// This reads json from file ans deserializes it into a significant incident object for use in the program
        /// </summary>
        /// <returns>
        /// A list of significant incidents
        /// </returns>
        public static List<ApplicationLayer.SignificantIncident> DeserializeSignificantIncidents()
        {
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\Significant-Incidents";
            string contents;
            bool empty;
            List<ApplicationLayer.SignificantIncident> sigList = new List<ApplicationLayer.SignificantIncident>();

            empty = IsDirectoryEmpty(location);

            if (!empty)
            {
                foreach (string file in Directory.EnumerateFiles(location, "*.json"))
                {
                    contents = File.ReadAllText(file);
                    ApplicationLayer.SignificantIncident deserializedIncident= JsonConvert.DeserializeObject<ApplicationLayer.SignificantIncident>(contents);
                    sigList.Add(deserializedIncident);
                }
            }

            return sigList;
        }



        /// <summary>
        /// This reads json from file ans deserializes it into a tweet object for use in the program
        /// </summary>
        /// <returns>
        /// A list of deserialized tweets
        /// </returns>
        public static List<ApplicationLayer.Tweet> DeserializeTweets()
        {
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\Tweets";
            string contents;
            bool empty;
            List<ApplicationLayer.Tweet> tweetList = new List<ApplicationLayer.Tweet>();

            empty = IsDirectoryEmpty(location);

            if (!empty)
            {
                foreach (string file in Directory.EnumerateFiles(location, "*.json"))
                {
                    contents = File.ReadAllText(file);
                    ApplicationLayer.Tweet deserializedTweet = JsonConvert.DeserializeObject<ApplicationLayer.Tweet>(contents);
#pragma warning disable CS8604 // Possible null reference argument.
                    tweetList.Add(deserializedTweet);
#pragma warning restore CS8604 // Possible null reference argument.
                }              
            }

            return tweetList;
        }



        /// <summary>
        /// Checks whether or not the specified path is empty or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns>
        /// True or False
        /// </returns>
        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }


        public static void GetDataFromFile(string filepath, string fileName, ApplicationLayer.App app)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            using (var stream = new StreamReader(filepath))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (line == null) continue;
                    var words = line.Split(',');
                    data.Add(words[0], words[1]);
                }
            }


           foreach (KeyValuePair<string, string> kvp in data)
           {
              string header = kvp.Key;
              string body = kvp.Value;

              app.ProcessSubmission(header, body);
           }


          

            
        }

        
     }
  
}
