using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBankingService.ApplicationLayer
{
    internal class App
    {
        private Dictionary<string, int> HashtagDict;
        private List<string> Mentions;
        private List<string> MessageList;
        private List<SignificantIncident> SIRList;
        private List<string> QuarantineList = new List<string>();
        private Dictionary<string, string> Abbreviations;
        private string URL;

        private string headerText;
        private string bodyText;
        private char type;
        private string emailAddress;
        private string dateString;

        private int TWITTER_LIMIT = 140;
        private int EMAIL_LIMIT = 1028;
        private int SMS_LIMIT = 140;


        public void ProcessSubmission(string header, string body)
        {
            
            type = ProcessHeader(header);
            ProcessBody(body, header, type);
           
        }

        /* Methods for Processing the Header Information */
        public char ProcessHeader(string header)
        {
            headerText = header;
            type = DetectType(header);
            return type;
        }


        public bool HeaderValid (string header)
        {
            bool valid = true;

            if (header.Length > 9)
            {
                MessageBox.Show("Header is too long - please input a valid message header", "Error" );
                valid = false;
            }

            if (header.Length < 9)
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



        /* Methods for Processing the Body Text */

        public string ProcessBody(string body, string header, char type)
        {
            switch (type)
            {
                case 'S':
                    break;
                case 'T':
                    break;
                case 'E':
                    Email e = new Email();
                    emailAddress = e.DetectEmailAddress(body);
                    dateString = e.DetectDate(body);
                    QuarantineList = e.DetectURL(body);
                    body = e.QuarantineURL(body, QuarantineList);
                    break;
            }

            return body;
        }



      


       

    }
}
