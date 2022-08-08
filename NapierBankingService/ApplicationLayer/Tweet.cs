namespace NapierBankingService.ApplicationLayer
{
    class Tweet : Message
    {
        protected Hashtag[] _hashTags;
        protected TwitterID[] _twitterIDs;

        public Tweet(string messageHeader, string messageBody, char messageType, string sender, int characterLimit, Hashtag[] hashTags, TwitterID[] twitterIDs) : base(messageHeader, messageBody, messageType, sender, characterLimit)
        {
            _hashTags = hashTags;
            _twitterIDs = twitterIDs;
        }


      
     

    }
}
