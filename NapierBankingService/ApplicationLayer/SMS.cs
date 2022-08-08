using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class SMS : Message
    {
      

        public SMS(string messageHeader, string messageBody, char messageType, string sender, int characterLimit) : base(messageHeader, messageBody, messageType, sender, characterLimit)
        {
            MessageHeader = messageHeader;
            MessageBody = messageBody;
            MessageType = messageType;
            Sender = sender;
            CharacterLimit = characterLimit;
        }



    }
}
