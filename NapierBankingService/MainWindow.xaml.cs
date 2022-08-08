
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace NapierBankingService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationLayer.App app;
        private string header;
        private string body;

        public string Header { get => header; set => header = value; }
        public string Body { get => body; set => body = value; }

        public MainWindow()
        {
            InitializeComponent();
            app = new ApplicationLayer.App();
        }
        
       

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Header = headerMessageBox.Text;
            Body = messageBody.Text;

            bool headerValid = app.HeaderValid(Header);

            if (!headerValid)
            {
                headerMessageBox.Clear();
            }
            

            app.ProcessSubmission(Header, Body);   
        }

        
        
    }
}
