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
    /// Interaction logic for Messages.xaml
    /// </summary>
    public partial class Messages : Window
    {

        string SMSOutput;
        List<String> SMSOutputList = new List<String>();
        public Messages(ApplicationLayer.App app)
        {
            InitializeComponent();


            foreach (ApplicationLayer.SMS s in app.SmsList)
            {
                SMSOutput = "HEADER: " + s.MessageHeader + "\nBODY: " + s.MessageBody + "\nPHONENUMBER: " + s.PhoneNumber + "\n ----------------------";
                SMSOutputList.Add(SMSOutput);
            }

            string smsOut = string.Join(Environment.NewLine, SMSOutputList);
            SMS_Text_Box.Text = smsOut;
        }

      
    }
}
