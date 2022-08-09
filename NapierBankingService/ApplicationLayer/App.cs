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
        private Dictionary<string, string> Abbreviations;
        

        private char type;
        

        private int smsTwitterLimit = 140;
        private int emailLimit = 1028;
        

        
        public int EmailLimit { get => emailLimit; set => emailLimit = value; }
        public int SmsTwitterLimit { get => smsTwitterLimit; set => smsTwitterLimit = value; }

        public void ProcessSubmission(string header, string body, string subject)
        {

            type = ProcessHeader(header);
            
            ProcessMessage(body, header, subject, type);
           
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
        public void ProcessMessage(string body, string header, string subject, char type)
        {
            switch (type)
            {
                case 'S':
                    break;
                case 'T':
                    break;
                case 'E':
                    object mail = ProcessEmail(body, subject, header, type);
                    Debug.WriteLine(mail.ToString());
                    break;
            }      
        }


        /// <summary>
        /// This method works through the steps to collect the necessary information about an email 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        public object ProcessEmail(string body, string subject, string header, char type)
        {
             /*Local Variables */
             string emailAddress;
             string dateString;
             bool incidentDetected;
             string incidentType;
             string sortCode;  
             List<string> QuarantineList = new List<string>();

             /* New Instance of Empty Email object*/
             Email e = new Email();

             emailAddress = e.DetectEmailAddress(body); //detects the email address from the body
             dateString = e.DetectDate(subject); //detects the date from the subject line
             incidentDetected = e.DetectIncident(subject); //detects whether or not the email is a significant incident (true or false)
             QuarantineList = e.DetectURL(body);
             body = e.QuarantineURL(body, QuarantineList);

            /* Creates new Instance of Significant Incident if Detected */
            if (incidentDetected)
            {
                SignificantIncident sig = new SignificantIncident();
                incidentType = sig.DetectIncidentType(subject);
                sortCode = sig.DetectSortCode(subject);
                SignificantIncident newMessage = new SignificantIncident(sortCode, incidentType, header, body, type, emailAddress, subject, dateString);
                return newMessage;
            }

            else if (!incidentDetected)
            {
                Email email = new Email(header, body, type, emailAddress, subject, dateString);
                return email;
            }

            else
            {
                return null;
            }
        }

    
    }
}
