using System;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics.CodeAnalysis;

namespace NapierBankingService
{
    /// <summary>
    /// Interaction logic for Messages.xaml
    /// </summary>

    [ExcludeFromCodeCoverage]
    public partial class Messages : Window
    {

        string? SMSOutput;
        List<string>? SMSOutputList = new List<string>();
        string? TweetOutput;
        List<string>? TweetOutputList = new List<string>();
        string? EmailOutput;
        List<string>? EmailOutputList = new List<string>();
        string? SigOutput;
        List<string>? SigOutputList = new List<string>();

        public Messages(ApplicationLayer.App app)
        {
            InitializeComponent();

            List<ApplicationLayer.SMS> sms = DataLayer.LoadData.DeserializeSMSs();
            List<ApplicationLayer.Tweet> tweets = DataLayer.LoadData.DeserializeTweets();
            List<ApplicationLayer.Email> emails = DataLayer.LoadData.DeserializeEmails();    
            List<ApplicationLayer.SignificantIncident> sig = DataLayer.LoadData.DeserializeSignificantIncidents();

            foreach (ApplicationLayer.SMS s in sms)
            {
                SMSOutput = "HEADER: " + s.MessageHeader + "\nBODY: " + s.MessageBody + "\nPHONENUMBER: " + s.PhoneNumber + "\n ----------------------";
                SMSOutputList.Add(SMSOutput);
            }

            foreach (ApplicationLayer.Tweet t in tweets)
            {
                TweetOutput = "HEADER: " + t.MessageHeader + "\nBODY: " + t.MessageBody + "\nTWITTER ID: " + t.Sender + "\n ----------------------";
                TweetOutputList.Add(TweetOutput);
            }

            foreach (ApplicationLayer.Email e in emails)
            {
                EmailOutput = "HEADER: " + e.MessageHeader + "\nBODY: " + e.MessageBody + "\nSENDER: " + e.Sender + "\n ----------------------";
                EmailOutputList.Add(EmailOutput);
            }

            foreach (ApplicationLayer.SignificantIncident s in sig)
            {
                SigOutput = "HEADER: " + s.MessageHeader + "\nBODY: " + s.MessageBody + "\nSENDER: " + s.Sender + "\nINCIDENT: " + s.IncidentType + "\nSORTCODE: " + s.SortCode + "\n----------------------";
                SigOutputList.Add(SigOutput); 
            }


            string smsOut = string.Join(Environment.NewLine, SMSOutputList);
            string tweetOut = string.Join(Environment.NewLine, TweetOutputList);
            string emailOut = string.Join(Environment.NewLine, EmailOutputList);
            string sigOut = string.Join(Environment.NewLine, SigOutputList);
            

            SMS_Text_Box.Text = smsOut;
            Tweets_Text_Box.Text = tweetOut;
            Emails_Text_Box.Text = emailOut;  
            SigIncidentsText.Text = sigOut;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }
        
    }
}
