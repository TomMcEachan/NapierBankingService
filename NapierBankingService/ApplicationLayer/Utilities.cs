using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    internal class Utilities
    {
        public static bool DetectIncident(string subject)
        {
            if (subject.Contains("SIR"))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
