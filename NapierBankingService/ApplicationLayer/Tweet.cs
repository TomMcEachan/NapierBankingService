using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace NapierBankingService.ApplicationLayer
{
    [Serializable]
    public class Tweet : Message
    {
        private List<Hashtag>? _hashTags;
        private List<Mention>? _mentions;
        

        public List<Hashtag>? HashTags { get => _hashTags; set => _hashTags = value; }
        public List<Mention>? Mentions { get => _mentions; set => _mentions = value; }
        

        public Tweet(string messageHeader, string messageBody, char messageType, string sender, List<Hashtag> hashTags, List<Mention> mentions) : base(messageHeader, messageBody, messageType, sender)
        {
            HashTags = hashTags;
            Mentions = mentions;
        }

        public Tweet() { }
        

        /// <summary>
        /// This method processes the Tweet by exanding the text speak and detecting the hashtags, mentions, and tweet sender
        /// </summary>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <param name="abbreviations"></param>
        /// <returns>
        /// A Tweet object with the necessary information from the form
        /// </returns>
        public static Tweet ProcessTweet(string body, string header, char type, Dictionary <string, string> abbreviations)
        {
            /* Local Variables */
            string sender;

            /* Instance of an Empty Tweet Object */
            Tweet t = new Tweet();

            body = Message.ExpandTextSpeak(body, abbreviations);
            sender = t.DetectTweetSender(body);
            List <Mention> mentionsList = t.DetectMentions(body);
            List<Hashtag> hashtagsList = t.DetectHashtags(body);

            Tweet tweet = new Tweet(header, body, type, sender, hashtagsList, mentionsList);

            return tweet;
        }


        /// <summary>
        /// This method uses regex to detect the hashtags in the body
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// A list of the detected hashtags
        /// </returns>
        public List<Hashtag> DetectHashtags (string body)
        {
            Regex rx = new Regex(@"#+[a-zA-Z0-9(_)]{1,}");
            List<Hashtag> hashtags = new List<Hashtag>();
            List<string> stringTags = new List<string>();

            MatchCollection matches = rx.Matches(body);

            //hashtags = matches.Cast<Match>().Select(m => m.Value).ToList();

            foreach (Match match in matches)
            {
                stringTags.Add(match.Value);     
            }

            foreach (string tag in stringTags)
            {
                Hashtag hash = new Hashtag(stringTags.ElementAt(0));          
                hashtags.Add(hash);
            }

            return hashtags;
        }


        /// <summary>
        /// This method uses regex to detect the mentions on the page
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// A list of the detected mentions
        /// </returns>
        public List<Mention> DetectMentions(string body)
        {
            Regex rx = new Regex(@"@+[a-zA-Z0-9(_)]{1,}");
            List<Mention> mentions = new List<Mention>();
            List<string> ids = new List<string>();

            MatchCollection matches = rx.Matches(body);

            //mentions = matches.Cast<Match>().Select(m => m.Value).ToList();

            foreach (Match match in matches)
            {
                ids.Add(match.Value);
            }

            foreach (string id in ids)
            {
                Mention mention = new Mention(id);
                mentions.Add(mention);
            }

            return mentions;
        }


        /// <summary>
        /// This method uses regex to detect the tweet sender
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// The tweet sender
        /// </returns>
        public string DetectTweetSender(string body)
        {
            Regex rx = new Regex(@"@+[a-zA-Z0-9(_)]{1,}");

            Match match = rx.Match(body);

            string mention = match.Value;

            Debug.WriteLine(mention);

            return mention;

        }


        /// <summary>
        /// This method takes a tweet list and collates a list of hashtags and adds them to a dictionary
        /// </summary>
        /// <param name="tweets"></param>
        /// <returns>
        /// A dictionary of the trending hashtags
        /// </returns>
        public static Dictionary<string, int> CollateHashtags(List<Tweet> tweets)
        {
            List<Hashtag> hashtags = new List<Hashtag>();
            List<string> allTags = new List<string>();
            
            
            if (tweets != null)
            {
                foreach (Tweet tweet in tweets)
                {
                    hashtags.Add(tweet.HashTags.ElementAt(0));
                }

                foreach (Hashtag tag in hashtags)
                {
                    allTags.Add(tag.Tag);
                }
            }
            
            var query = allTags.GroupBy(x => x)
                                .ToDictionary(y => y.Key, y => y.Count())
                                .OrderByDescending(z => z.Value);

            Dictionary<string, int> dict = new Dictionary<string, int>();
            
            
            foreach (var x in query)
            {
                dict.Add(x.Key, x.Value);
            }

            foreach (KeyValuePair<string, int> kvp in dict)
            {
                Debug.WriteLine("Hashtag: {0} ---- Times Used: {1}", kvp.Key, kvp.Value);
            }
            
            return dict;
        }
     

        /// <summary>
        /// This method takes a list of tweets and collates the mentions together
        /// </summary>
        /// <param name="tweets"></param>
        /// <returns>
        /// A string list of mentions
        /// </returns>
        public static List<string> CollateMentions(List<Tweet> tweets)
        {
            List<Mention> IDs = new List<Mention>();
            List<string> stringIDs = new List<string>();


            foreach (Tweet tweet in tweets)
            {
                IDs.Add(tweet.Mentions.ElementAt(0));
            }

            foreach (Mention mention in  IDs)
            {
                stringIDs.Add(mention.UserID);
            }

            return stringIDs;
        }

    }
}
