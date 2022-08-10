using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace NapierBankingService.ApplicationLayer
{
    public class Email : Message
    {
       
        private string messageSubject;
        private string dateString;
        private string emailAddress;
        

        protected string MessageSubject { get => messageSubject; set => messageSubject = value; }


        /* Email Constructor */
        public Email(string messageHeader, string messageBody, char messageType, string sender,  string messageSubject, string date) : base(messageHeader, messageBody, messageType, sender)
        {  
            MessageSubject = messageSubject;
        }

        /* Empty Email Constructor */
        public Email () {  }


        /* Email Specific Methods Needed to Create the Object */

        /// <summary>
        /// This method takes the subject added by the user and returns the date detected
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// Date detected
        /// </returns>
        public string DetectDate(string subject)
        {
            Regex rx = new Regex("(SIR)\\s*([0-2][1-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/([0-9][0-9])");

            MatchCollection matches = rx.Matches(subject);

            foreach (Match match in matches)
            {
                dateString = match.Value;
            }

            return dateString;
        }

        /// <summary>
        /// This method takes the body added by the user and returns the email address detected
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// An email address
        /// </returns>
        public string DetectEmailAddress(string body)
        {
            Regex rx = new Regex("[a-zA-Z0-9.()]{1,}@[a-zA-Z0-9()]{1,}.[a-zA-Z.(_)]{1,}");

            MatchCollection matches = rx.Matches(body);

            foreach (Match match in matches)
            {
                emailAddress = match.Value;
            }

            return emailAddress;
        }

        /// <summary>
        /// This method takes the body added by the user and returns a list of the URLs detected
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// A list of URLs detected
        /// </returns>
        public List<string> DetectURL(string body)
        {
            List<string> list;

            Regex rx = new Regex(@"((http|ftp|https|HTTPS|HTTP|FTP):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");

            MatchCollection matches = rx.Matches(body);

            list = matches.Cast<Match>().Select(m => m.Value).ToList();

            return list;
        }

        /// <summary>
        /// This method takes the body added by the user, and a list of URLs to be quarantined and removes the URLs from the body
        /// </summary>
        /// <param name="body"></param>
        /// <param name="QuarantineList"></param>
        /// <returns>
        /// A new version of body with the URLs replaced with the text "URL Quarantined" in triangle brackets
        /// </returns>
        public string QuarantineURL(string body, List<string> QuarantineList) 
        {
            foreach (string URL in QuarantineList)
            {
              body = body.Replace(URL, "<URL Quarantined>");
            }
            
            return body;
        }


        /// <summary>
        /// This method works through the steps to collect the necessary information about an email 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        public static Email ProcessEmail(string body, string subject, string header, char type)
        {
            /*Local Variables */
            string emailAddress;
            string dateString;
            List<string> QuarantineList = new List<string>();

            /* New Instance of Empty Email object*/
            Email e = new Email();

            emailAddress = e.DetectEmailAddress(body); //detects the email address from the body
            dateString = e.DetectDate(subject); //detects the date from the subject line
            QuarantineList = e.DetectURL(body);
            body = e.QuarantineURL(body, QuarantineList);
            Email email = new Email(header, body, type, emailAddress, subject, dateString);

            return email;

        }




    }
}
