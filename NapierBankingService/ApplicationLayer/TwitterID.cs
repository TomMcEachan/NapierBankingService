using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    internal class TwitterID
    {
        public string twitterID { get; set; }

        public TwitterID (string twitterID)
        {
            this.twitterID = twitterID;
        }
    }
}
