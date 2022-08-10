using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace NapierBankingService.DataLayer
{
    public class LoadData
    {

        public static Dictionary<string, string> ReadTextWordsCSV()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            using (var stream = new StreamReader(@"Assets\textwords.csv"))
            {
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (line == null) continue;
                    var words = line.Split(',');
                    dict.Add(words[0], words[1]);
                }
            }

            return dict;
        }

        public static void DeserializeEmail()
        {
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\NapierBankingSystem\Emails";
           

           List<ApplicationLayer.Email> emailList = new List<ApplicationLayer.Email>();

                
           
        } 

        
     }
  
}
