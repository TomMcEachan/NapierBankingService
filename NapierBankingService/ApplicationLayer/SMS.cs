using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class SMS : Message
    {

        private string phoneNumber;
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }


        
        public SMS(string messageHeader, string messageBody, char messageType, string sender) : base(messageHeader, messageBody, messageType, sender)
        {

        }

        public SMS() { }




        /// <summary>
        /// This method processes the SMS by expanding the textspeak and detecting the phonenumber
        /// </summary>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <param name="abbreviations"></param>
        /// <returns>
        /// An SMS object with the necessary information from the form
        /// </returns>
        public static SMS ProcessSMS (string body, string header, char type, Dictionary <string, string> abbreviations)
        {
            /* Local Variables */
            string sender;

            /* Instance of Empty SMS Object */
            SMS s = new SMS();

            
            body = Message.ExpandTextSpeak(body, abbreviations); // expands the body
            sender = s.DetectPhoneNumber(body); //detects the message sender


            SMS sms = new SMS(header, body, type, sender);// creates an sms object with the information required 

            return sms; 

        }



        /// <summary>
        /// This method uses regex to detect the phone number
        /// </summary>
        /// <param name="body"></param>
        /// <returns>
        /// The detected phone number as a string
        /// </returns>
        public string DetectPhoneNumber (string body)
        {
            Regex rx = new Regex(@"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\d{1,14}$");

            MatchCollection matches = rx.Matches(body);

            foreach (Match match in matches)
            {
                PhoneNumber = match.Value;
            }

            Debug.WriteLine(PhoneNumber);

            return PhoneNumber;
        }

    }
}
