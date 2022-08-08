using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBankingService.ApplicationLayer
{
    internal class App
    {
        Dictionary<string, int> HashtagDict;
        List<string> Mentions;
        List<string> MessageList;
        List<SignificantIncident> SIRList;
        List<string> QuarantineList;
        Dictionary<string, string> Abbreviations;

        string headerText;
        string bodyText;
        char type;

        int TWITTER_LIMIT = 140;
        int EMAIL_LIMIT = 1028;
        int SMS_LIMIT = 140;


        public void ProcessSubmission(string header, string body)
        {
            
            type = ProcessHeader(header);
            ProcessBody(body, type);
           
        }

        /* Methods for Processing the Header Information */
        public char ProcessHeader(string header)
        {
            headerText = header;
            type = DetectType(header);

            switch(type)
            {
                case 'S':
                    Trace.WriteLine("SMS");
                    break;
                case 'T':
                    Trace.WriteLine("Tweet");
                    break;
                case 'E':
                    Trace.WriteLine("Email");
                    break;
            }

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

        public void ProcessBody(string body, char type)
        {
            
            

        }


        


    }
}
