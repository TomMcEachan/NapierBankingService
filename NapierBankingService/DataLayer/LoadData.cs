using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.DataLayer
{
    public class LoadData
    {

        public string Abbreviation { get; set; }
        public string FullText { get; set; }


        void ReadCSV()
        {
            using (var stream = new StreamReader(@"Assets\textwords.csv"))
            using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<LoadData>();
            }

        }

        
     }
  
}
