namespace NapierBankingService.ApplicationLayer
{
    class Tweet : Message
    {
        protected Hashtag[] _hashTags;
        protected TwitterID[] _twitterIDs;

        public Tweet(string messageHeader, string messageBody, char messageType, string sender, Hashtag[] hashTags, TwitterID[] twitterIDs) : base(messageHeader, messageBody, messageType, sender)
        {
            _hashTags = hashTags;
            _twitterIDs = twitterIDs;
        }


      
     

    }
}
