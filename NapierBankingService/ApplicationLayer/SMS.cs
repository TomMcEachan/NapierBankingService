using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class SMS : Message
    {
        protected string _messageHeader;
        protected string _messageBody;
        protected char _messageType;
        protected string _sender;
        protected int _characterLimit;



        public SMS(string messageHeader, string messageBody, char messageType, string sender, int characterLimit)
        {
            _messageHeader = messageHeader;
            _messageBody = messageBody;
            _messageType = messageType;
            _sender = sender;
            _characterLimit = characterLimit;
        }



    }
}
