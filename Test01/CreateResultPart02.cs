using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Test01.Model;

namespace Test01
{
    public class CreateResultPart02
    {
        private string pattern = @"[^a-zA-Z0-9üäöÜÖÄß-]+";
        
        public string GenerateStellenchaOSURL(Anzeigen anzeigen)
        {
            string company = ReplaceInvalidCharacters(anzeigen.Company);
            string title = ReplaceInvalidCharacters(anzeigen.Title);
            string result = $"https://www.stellencha.os/stellen/{company}/{anzeigen.Id}/{title}/";
            result = RemoveSpace(result);

            return result;
        }

        public string GenerateStellenchaOSTitle(Anzeigen anzeigen)
        {
            string region = string.Join(", ", anzeigen.Region);
            return $"{anzeigen.Title} | {anzeigen.Company} | {region} | Stellencha.os";
        }

        public string GenerateJobsMitBizURL(Anzeigen anzeigen)
        {
            string region = string.Join("-", anzeigen.Region).ToLower();
            string title = ReplaceInvalidCharacters(anzeigen.Title);
            string result = $"https://www.jobs-mit.biz/jobs/{region}/{anzeigen.Id}/{title}/";
            result = RemoveSpace(result);

            return result;
        }

        public string GenerateJobsMitBizTitle(Anzeigen anzeigen)
        {
            string region = string.Join(", ", anzeigen.Region);
            return $"Apply now! - {anzeigen.Title} @ {region}";
        }

        public string GenerateJobDealerURL(Anzeigen anzeigen)
        {
            string company = ReplaceInvalidCharacters(anzeigen.Company);
            string region = string.Join("-", anzeigen.Region).ToLower();
            string title = ReplaceInvalidCharacters(anzeigen.Title);
            string result = $"https://www.job.dealer/job/{anzeigen.Id}-{title}-bei-{company}-in-{region}";
            result = RemoveSpace(result);

            return result;
        }

        public string GenerateJobDealerTitle(Anzeigen anzeigen)
        {
            return $"{anzeigen.Title} at {anzeigen.Company} in {string.Join(", ", anzeigen.Region)} from your job.dealer";
        }
        
        private string RemoveSpace(string inputString)
        {
            var result = inputString.Replace(' ', '-');
            while (result.Contains("--"))
            {
                result = result.Replace("--", "-");
            }

            return result;
        }

        private string ReplaceInvalidCharacters(string inputString)
        {
            Regex regex = new Regex(pattern);
            string result = regex.Replace(inputString, "-");
            return result;
        }
    }
}
