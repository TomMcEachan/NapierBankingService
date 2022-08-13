namespace NapierBankingService.ApplicationLayer
{
    public class Mention
    {
        private string _mention;

        public string UserID { get => _mention; set => _mention = value; }


        public Mention(string mention)
        {
            UserID = mention;
        }

    }
}
