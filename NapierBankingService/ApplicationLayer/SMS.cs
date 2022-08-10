using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class SMS : Message
    {
      
        public SMS(string messageHeader, string messageBody, char messageType, string sender) : base(messageHeader, messageBody, messageType, sender)
        {
                 
        }


        public static SMS ProcessSMS (string body, string subject, string header, char type, Dictionary <string, string> abbreviations)
        {
            /* Local Variables */
            string sender = string.Empty;


            body = Utilities.ExpandTextSpeak(body, abbreviations);




            SMS sms = new SMS(header, body, type, sender);


            return sms; 

        }
    }
}
