namespace NapierBankingService.ApplicationLayer
{
    public class Email : Message
    {
       
        private string messageSubject;
        private string emailAddress;

        protected string MessageSubject { get => messageSubject; set => messageSubject = value; }

        public Email(string messageHeader, string messageBody, char messageType, string sender, int characterLimit, string messageSubject) : base(messageHeader, messageBody, messageType, sender, characterLimit)
        {  
            MessageSubject = messageSubject;
        }


        public Email () {  }

        public void QuaraintineURL()
        {

        }


        

    }
}
