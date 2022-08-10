using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankingService.ApplicationLayer
{
    public class Utilities
    {

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


        /// <summary>
        /// This method takes a body and expands it based on the abbreviations found in the dictionary list
        /// </summary>
        /// <param name="body"></param>
        /// <param name="abbreviations"></param>
        /// <returns>
        /// An expanded version of the body added by the user
        /// </returns>
        public static string ExpandTextSpeak(string body, Dictionary<string, string> abbreviations)
        {
            string expandedBody;

            expandedBody = string.Join(" ", body.Split(' ').Select(i => abbreviations.ContainsKey(i) ? abbreviations[i] : i));
            return expandedBody;

        }
    }
}
