using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class SignificantIncident : Email
    {
        private string _sortCode;
        private string _incidentType;
        List<string> incidentTypes = new List<string>();

        public string IncidentType { get => _incidentType; set => _incidentType = value; }
        public string SortCode { get => _sortCode; set => _sortCode = value; }

        public SignificantIncident (string sortCode, string incidentType, string messageHeader, string messageBody, char messageType, string sender, string messageSubject, string date) : base (messageHeader, messageBody, messageType, sender,  messageSubject, date)
        {
            IncidentType = incidentType;
            SortCode = sortCode; 
        }

        public SignificantIncident () { }



        /* Methods for Detecting Incident */

        public string DetectIncidentType (string subject)
        {

            if (subject.Contains("Theft"))
            {
                IncidentType = "Theft";
            }

            if (subject.Contains("Staff Attack"))
            {
                IncidentType = "Staff Attack";
            }

            if (subject.Contains("ATM Theft"))
            {
                IncidentType = "ATM Theft";
            }

            if (subject.Contains("Raid"))
            {
                IncidentType = "Raid";
            }

            if (subject.Contains("Customer Attack"))
            {
                IncidentType = "Customer Attack";
            }

            if (subject.Contains("Staff Aube"))
            {
                IncidentType = "Staff Abuse";
            }

            if (subject.Contains("Bomb Threat"))
            {
                IncidentType = "Bomb Threat";
            }

            if (subject.Contains("Terrorism"))
            {
                IncidentType = "Terrorism";
            }

            if (subject.Contains("Suspicious Incident"))
            {
                IncidentType = "Suspicious Incident";
            }

            if (subject.Contains("Intelligence"))
            {
                IncidentType = "Intelligence";
            }

            if (subject.Contains("Cash Loss"))
            {
                IncidentType = "Cash Loss";
            }

            return IncidentType;
        }

        public string DetectSortCode(string subject)
        {
            Regex rx = new Regex(@"\b[0-9]{2}-?[0-9]{2}-?[0-9]{2}\b");

            MatchCollection matches = rx.Matches(subject);

            foreach(Match match in matches)
            {
                SortCode = match.Value;               
            }

            return SortCode;

        }


        public static SignificantIncident ProcessSignificantIncident(string body, string subject, string header, char type)
        {
            string emailAddress;
            string dateString;
            string incidentType;
            string sortCode;
            List<string> QuarantineList = new List<string>();

            SignificantIncident sig = new SignificantIncident();
            incidentType = sig.DetectIncidentType(subject);
            sortCode = sig.DetectSortCode(subject);
            emailAddress = sig.DetectEmailAddress(body); //detects the email address from the body
            dateString = sig.DetectDate(subject); //detects the date from the subject line
            QuarantineList = sig.DetectURL(body);
            body = sig.QuarantineURL(body, QuarantineList);

            SignificantIncident newMessage = new SignificantIncident(sortCode, incidentType, header, body, type, emailAddress, subject, dateString);
            return newMessage;
        }



    }
}
