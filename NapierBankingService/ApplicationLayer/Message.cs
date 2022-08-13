using System.Collections.Generic;

namespace NapierBankingService.ApplicationLayer
{
    public class Message
    {
        private string _messageHeader;
        private string _messageBody;
        private char _messageType;
        private string _sender;
       

        public string MessageHeader { get => _messageHeader; set => _messageHeader = value; }
        public string MessageBody { get => _messageBody; set => _messageBody = value; }
        public char MessageType { get => _messageType; set => _messageType = value; }
        public string Sender { get => _sender; set => _sender = value; }
        

        public Message(string messageHeader, string messageBody, char messageType, string sender)
        {
            MessageHeader = messageHeader;
            MessageBody = messageBody;
            MessageType = messageType;
            Sender = sender;
        }

        public Message () { }


        /// <summary>
        /// This method takes a body and expands it based on the abbreviations found in the dictionary list
        /// </summary>
        /// <param name="body"></param>
        /// <param name="abbreviations"></param>
        /// <returns>
        /// An expanded version of the body added by the user
        /// </returns>
        public static string ExpandTextSpeak(string body, Dictionary<string, string> abbreviations)
        {
            foreach (string abbreviation in abbreviations.Keys)
            {
                body = body.Replace(abbreviation, abbreviation + " <" + abbreviations[abbreviation] + ">");
            }

            return body;
        }
    }
}
