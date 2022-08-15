using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class Hashtag
    {
        private string? _hashtag;

        public string? Tag { get => _hashtag; set => _hashtag = value; }


        public Hashtag (string hashtag)
        {
           Tag = hashtag;
        }

        
    }
}

