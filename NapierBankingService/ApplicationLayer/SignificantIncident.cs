using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class SignificantIncident
    {
        int _sortCode;
        string _incidentType;

        public SignificantIncident (int sortCode, string incidentType)
        {
            _incidentType = incidentType;
            _sortCode = sortCode; 
        }


    }
}
