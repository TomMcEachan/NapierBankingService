using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class Hashtag
    {
        public string hashtagText { get; set; }

        public Hashtag(string hashtagText)
        {
           this.hashtagText = hashtagText;
        }

    }
}
