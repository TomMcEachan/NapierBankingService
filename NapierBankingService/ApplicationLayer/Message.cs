namespace NapierBankingService.ApplicationLayer
{
    public class Message
    {
        protected string _messageHeader;
        protected string _messageBody;
        protected char _messageType;
        protected string _sender;
        protected int _characterLimit;

        public Message(string messageHeader, string messageBody, char messageType, string sender, int characterLimit)
        {
            _messageHeader = messageHeader;
            _messageBody = messageBody;
            _messageType = messageType;
            _sender = sender;
            _characterLimit = characterLimit;
        }

        public void QuaraintineURL()
        {

        }

        public void ProcessMessage()
        {

        }





    }
}
