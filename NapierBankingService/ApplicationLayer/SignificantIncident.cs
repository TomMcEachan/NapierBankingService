using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class SignificantIncident : Email
    {
        int _sortCode;
        string _incidentType;
        List<string> incidentTypes = new List<string>();

        public SignificantIncident (int sortCode, string incidentType)
        {
            _incidentType = incidentType;
            _sortCode = sortCode; 
        }

        public SignificantIncident () { }



        /* Methods for Detecting Incident */

        public void DetectIncidentType (string subject)
        {

        }

        public void DetectSortCode(string subject)
        {

        }



    }
}
