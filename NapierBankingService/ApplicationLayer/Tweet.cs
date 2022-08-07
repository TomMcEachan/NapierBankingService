namespace NapierBankingService.ApplicationLayer
{
    class Tweet : Message
    {
        protected string _messageHeader;
        protected string _messageBody;
        protected char _messageType;
        protected string _sender;
        protected int _characterLimit;
        protected Hashtag[] _hashTags;
        protected TwitterID[] _twitterIDs;



        public Tweet(string messageHeader, string messageBody, char messageType, string sender, int characterLimit, Hashtag[] hashTags, TwitterID[] twitterIDs) : base(messageHeader, messageBody, messageType, sender, characterLimit)
        {
            _messageHeader = messageHeader;
            _messageBody = messageBody;
            _messageType = messageType;
            _sender = sender;
            _characterLimit = characterLimit;
            _hashTags = hashTags;
            _twitterIDs = twitterIDs;
        }


      
     

    }
}
