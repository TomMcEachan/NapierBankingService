namespace NapierBankingService.ApplicationLayer
{
    public class Message
    {
        private string _messageHeader;
        private string _messageBody;
        private char _messageType;
        private string _sender;
        private int _characterLimit;

        public string MessageHeader { get => _messageHeader; set => _messageHeader = value; }
        public string MessageBody { get => _messageBody; set => _messageBody = value; }
        public char MessageType { get => _messageType; set => _messageType = value; }
        public string Sender { get => _sender; set => _sender = value; }
        public int CharacterLimit { get => _characterLimit; set => _characterLimit = value; }

        public Message(string messageHeader, string messageBody, char messageType, string sender)
        {
            MessageHeader = messageHeader;
            MessageBody = messageBody;
            MessageType = messageType;
            Sender = sender;
        }

        public Message () { }

        public void ExpandTextSpeak()
        {

        }    
    }
}
