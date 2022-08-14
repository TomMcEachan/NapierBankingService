using System;
using System.Collections.Generic;
using System.Windows;

namespace NapierBankingService
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        public Results(ApplicationLayer.App app)
        {
            InitializeComponent();
        
            Tuple<string, List<string>, string> tuple = app.EndSession(app.HashtagDict, app.MentionsList, app.SirStringList);

            string trendingList = tuple.Item1;
            List<string> mentions = tuple.Item2;
            string sigs = tuple.Item3;

            

            if(trendingList != null && mentions != null && sigs != null)
            {
                TrendingTextBox.Text = trendingList;
                SignificantIncidentsTextBox.Text = String.Join(Environment.NewLine, sigs);
                MentionsTextBox.Text = String.Join(Environment.NewLine, mentions);
            }
            
        }

       
    }
}
