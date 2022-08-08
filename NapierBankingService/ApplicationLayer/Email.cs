using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class Email : Message
    {
       
        private string messageSubject;

        protected string MessageSubject { get => messageSubject; set => messageSubject = value; }

        public Email(string messageHeader, string messageBody, char messageType, string sender, int characterLimit, string messageSubject) : base(messageHeader, messageBody, messageType, sender, characterLimit)
        {  
            MessageSubject = messageSubject;
        }






    }
}
