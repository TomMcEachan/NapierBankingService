
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
        private string subject;

        public string Header { get => header; set => header = value; }
        public string Body { get => body; set => body = value; }
        public string Subject{ get => subject; set => subject = value; }

        public MainWindow()
        {
            InitializeComponent();
            app = new ApplicationLayer.App();
        }
        
       

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Header = headerMessageBox.Text;
            Body = messageBody.Text;
            Subject = subjectLine.Text;

            bool headerValid = app.HeaderValid(Header);

            if (!headerValid)
            {
                headerMessageBox.Clear();
            }
            
            app.ProcessSubmission(Header, Body, Subject);   
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void headerMessageBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (headerMessageBox.Text.Contains("E"))
            {
                subjectLine.Visibility = Visibility.Visible;
                subjectLabel.Visibility = Visibility.Visible;
                messageBody.Margin = new Thickness(195, 185, 0, 0);
                messageTextLabel.Margin = new Thickness(54, 185, 0, 0);
            }

            if (headerMessageBox.Text.Contains("S") || headerMessageBox.Text.Contains("T") || !headerMessageBox.Text.Contains("E"))
            {
                subjectLine.Visibility = Visibility.Collapsed;
                subjectLabel.Visibility = Visibility.Collapsed;
                messageBody.Margin = new Thickness(195, 154, 0, 0);
                messageTextLabel.Margin = new Thickness(49, 154, 0, 0);
            }
        }

        private void messageBody_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
