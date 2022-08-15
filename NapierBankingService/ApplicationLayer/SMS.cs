using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NapierBankingService.ApplicationLayer
{
    public class SMS : Message
    {

        private string phoneNumber;
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }


        
        public SMS(string messageHeader, string messageBody, char messageType, string sender) : base(messageHeader, messageBody, messageType, sender)
        {
            PhoneNumber = sender;
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
            Regex rx = new Regex(@"((\+[ /]*)?(\d[ /]*){10,11}\d)");

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
