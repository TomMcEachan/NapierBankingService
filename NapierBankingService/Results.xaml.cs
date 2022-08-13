using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        
            Tuple<string, string, string> tuple = app.EndSession(app.HashtagDict);

            string trendingList = tuple.Item1;

            TrendingTextBox.Text = trendingList;
        }

       
    }
}
