﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace NapierBankingService.DataLayer
{
    public class LoadData
    {

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
                    tweetList.Add(deserializedTweet);
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

        
     }
  
}
