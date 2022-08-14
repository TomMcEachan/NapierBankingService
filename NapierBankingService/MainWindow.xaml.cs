
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace NapierBankingService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationLayer.App app = new ApplicationLayer.App();
        private string header;
        private string body;
        
        

        public string Header { get => header; set => header = value; }
        public string Body { get => body; set => body = value; }
        

        public MainWindow()
        {
            InitializeComponent();
            app.StartUp();
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
                }
            }
              
#pragma warning disable CS8604 // Possible null reference argument.
            app.ProcessSubmission(Header, Body);   
#pragma warning restore CS8604 // Possible null reference argument.
        }

        

        private void headerMessageBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (headerMessageBox.Text.Contains("E"))
            {
                messageBody.MaxLength = app.EmailLimit;
             
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
                DataLayer.LoadData.GetDataFromFile(filePath, fileName, app);
            }
        
        }
    }
}
