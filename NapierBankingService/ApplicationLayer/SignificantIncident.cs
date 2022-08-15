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
        private string? _sortCode;
        private string? _incidentType;
        

        public string? IncidentType { get => _incidentType; set => _incidentType = value; }
        public string? SortCode { get => _sortCode; set => _sortCode = value; }

        public SignificantIncident (string sortCode, string incidentType, string messageHeader, string messageBody, char messageType, string sender, string messageSubject, string date) : base (messageHeader, messageBody, messageType, sender,  messageSubject, date)
        {
            IncidentType = incidentType;
            SortCode = sortCode; 
        }

        public SignificantIncident () { }



        /* Methods for Detecting Incident */


        /// <summary>
        /// This method detects the kind of specific incident by matching the subject with the relevant word
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>
        /// Returns a string of the incident type
        /// </returns>
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

            if (subject.Contains("Staff Abuse"))
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


        /// <summary>
        /// This method uses regex to detect a sort code
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>
        /// A sort code
        /// </returns>
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

        /// <summary>
        /// This method processes the significant incident and adds the relevant information to an object
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="header"></param>
        /// <param name="type"></param>
        /// <returns>
        /// A significant incident object with the relevant information
        /// </returns>
        public static SignificantIncident ProcessSignificantIncident(string body, string header, char type)
        {
            string emailAddress;
            string dateString;
            string incidentType;
            string sortCode;
            string subject;
            List<string> QuarantineList;

            SignificantIncident sig = new SignificantIncident();

            subject = sig.DetectSubjectLine(body);

            incidentType = sig.DetectIncidentType(subject);
            sortCode = sig.DetectSortCode(subject);
            emailAddress = sig.DetectEmailAddress(body); //detects the email address from the body
            dateString = sig.DetectDate(subject); //detects the date from the subject line
            QuarantineList = sig.DetectURL(body);
            body = sig.QuarantineURL(body, QuarantineList);

            SignificantIncident newMessage = new SignificantIncident(sortCode, incidentType, header, body, type, emailAddress, subject, dateString);
            return newMessage;
        }


        /// <summary>
        /// This method checks an email subject line to see if the email can be considered a significant incident
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>
        /// True or False
        /// </returns>
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


        public static Dictionary<string, string> CollateSignificantIncidents(List<SignificantIncident> sigs)
        {
             
            Dictionary<string, string> incidents = new Dictionary<string, string>();   

            foreach (SignificantIncident sig in sigs)
            {
                incidents.Add(sig.SortCode, sig.IncidentType);
            }

            return incidents;
        }






    }
}
