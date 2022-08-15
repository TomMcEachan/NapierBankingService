using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace NapierBankingService.ApplicationLayer
{
    public class Email : Message
    {
       
        private string? messageSubject;
        private string? dateString;
        private string? emailAddress;
        private string? subject;

        protected string? MessageSubject { get => messageSubject; set => messageSubject = value; }
        public string? Subject { get => subject; set => subject = value; }
        public string? DateString { get => dateString; set => dateString = value; }


        /* Email Constructor */
        public Email(string messageHeader, string messageBody, char messageType, string subject, string sender, string date) : base(messageHeader, messageBody, messageType, sender)
        {
            Subject = subject;
            DateString = date;
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
            Regex rx = new(@"[0-1]?[0-9]/[0-9]{2}/[0-9]{4}");

            MatchCollection matches = rx.Matches(subject);

            foreach (Match match in matches)
            {
                DateString = match.Value;
            }

            if (DateString != null)
            {
                return DateString;
            }
            
            else
            {
                return "no date";
            }
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
            Regex rx = new("[a-zA-Z0-9.()]{1,}@[a-zA-Z0-9()]{1,}.[a-zA-Z.(_)]{1,}");

            MatchCollection matches = rx.Matches(body);

            foreach (Match match in matches)
            {
                emailAddress = match.Value;
            }

            if (emailAddress != null)
            {
                return emailAddress;
            }

            else
            {
                return "no email";
            }
      
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

            Regex rx = new(@"((http|ftp|https|HTTPS|HTTP|FTP):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");

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

        public string DetectSubjectLine(string body)
        {
            Regex rx = new("(Subject:)+.*?(?=Body:)");

            Match match = rx.Match(body);

            string subjectLine = match.Value;

            return subjectLine;

        }


        public string DetectBodyText(string body)
        {
            Regex rx = new("(Body:)+.*");

            Match match = rx.Match(body);

            string bodyText = match.Value;

            return bodyText;
        }



        /// <summary>
        /// This method works through the steps to collect the necessary information about an email 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        public static Email ProcessEmail(string body, string header, char type)
        {
            /*Local Variables */
            string emailAddress;
            string dateString;
            string subject;
            List<string> QuarantineList;

            /* New Instance of Empty Email object*/
            Email e = new();

            subject = e.DetectSubjectLine(body);
            emailAddress = e.DetectEmailAddress(body); //detects the email address from the body
            dateString = e.DetectDate(subject); //detects the date from the subject line
            body = e.DetectBodyText(body);
            QuarantineList = e.DetectURL(body);
            body = e.QuarantineURL(body, QuarantineList);
            Email email = new(header, body, type, subject, emailAddress, dateString);

            return email;

        }



       




    }
}


