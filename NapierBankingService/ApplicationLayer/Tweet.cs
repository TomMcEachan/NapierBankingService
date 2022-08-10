using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace NapierBankingService.ApplicationLayer
{
    public class Tweet : Message
    {
        private List<string> _hashTags;
        private List<string> _mentions;

        public List<string> HashTags { get => _hashTags; set => _hashTags = value; }
        public List<string> Mentions { get => _mentions; set => _mentions = value; }

        public Tweet(string messageHeader, string messageBody, char messageType, string sender, List<string> hashTags, List<string> mentions) : base(messageHeader, messageBody, messageType, sender)
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
            List <string> mentionsList = t.DetectMentions(body);
            List<string> hashtagsList = t.DetectHashtags(body);

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
        public List<string> DetectHashtags (string body)
        {
            Regex rx = new Regex(@"#+[a-zA-Z0-9(_)]{1,}");
            List<string> hashtags;

            MatchCollection matches = rx.Matches(body);

            hashtags = matches.Cast<Match>().Select(m => m.Value).ToList();

            return hashtags;
        }


        /// <summary>
        /// This method uses regex to detect the mentions on the page
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// A list of the detected mentions
        /// </returns>
        public List<string> DetectMentions(string body)
        {
            Regex rx = new Regex(@"@+[a-zA-Z0-9(_)]{1,}");
            List<string> mentions;

            MatchCollection matches = rx.Matches(body);

            mentions = matches.Cast<Match>().Select(m => m.Value).ToList();

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
     

    }
}
