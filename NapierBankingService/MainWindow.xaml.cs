
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace NapierBankingService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        ApplicationLayer.App app = new ApplicationLayer.App();
        private string? header;
        private string? body;
        
        

        public string? Header { get => header; set => header = value; }
        public string? Body { get => body; set => body = value; }
        

        public MainWindow()
        {
            InitializeComponent();
            app.StartUp();
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Header = headerMessageBox.Text;
            Body = messageBody.Text;          

            if (Header!= null &&  Body!= null)
            {
                bool headerValid = app.HeaderValid(Header);

                if (!headerValid)
                {
                   headerMessageBox.Clear();
                    MessageBox.Show("This header is not valid. Please input a valid header that matches the requirements e.g, E123456789");
                }
            }
              
            app.ProcessSubmission(Header, Body);   

        }

        

        private void headerMessageBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (headerMessageBox.Text.Contains("E"))
            {
                messageBody.MaxLength = app.EmailLimit;
                messageBody.Text = "From:\nSubject:\nBody:\n";
             
            }

            if (headerMessageBox.Text.Contains("S") || headerMessageBox.Text.Contains("T") || !headerMessageBox.Text.Contains("E"))
            {
                messageBody.MaxLength = app.SmsTwitterLimit;
            }
        }

        private void End_Session(object sender, RoutedEventArgs e)
        {
            Results Results = new Results(app);
            this.Close();
            Results.Show();
        }

        private void Upload_File_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            
            bool? response = ofd.ShowDialog();

            if(response == true)
            {
                string filePath = ofd.FileName;
                string fileName = Path.GetFileName(filePath);
                DataLayer.LoadData.GetDataFromFile(filePath, app);
            }
        
        }

        private void View_Messages_Button_Click(object sender, RoutedEventArgs e)
        {
            Messages Messages = new Messages(app);
            this.Close();
            Messages.Show();
        }
    }
}
