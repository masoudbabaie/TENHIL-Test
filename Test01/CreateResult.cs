using System;
using Test01.Model;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Test01
{
    public class CreateResult
    {
        //Anzeigen infos = new Anzeigen();
        private string pattern = @"[^a-zA-Z0-9üäöÜÖÄß-]+";
        public List<ResultViewModel> GetAnzeigens(List<Anzeigen> lstAnzeigens)
        {
            List<ResultViewModel> lstResultViewModels = new List<ResultViewModel>();
            ResultViewModel resultViewModel;

            foreach (var anzeigen in lstAnzeigens)
            {
                resultViewModel = new ResultViewModel();
                resultViewModel = GenerateAnzeigen(anzeigen);

                lstResultViewModels.Add(resultViewModel);
            }
            
            return (lstResultViewModels);
        }

        private ResultViewModel GenerateAnzeigen (Anzeigen anzeigen)
        {
            var resultViewModel = new ResultViewModel();

            string firstPartUrl = "https://www.stellencha.os/stellen/";
            
            string result = ($"{firstPartUrl}/{ReplaceInvalidCharacters(anzeigen.Company.Trim())}/{anzeigen.Id.ToString()}/{ReplaceInvalidCharacters(anzeigen.Title.Trim())}/");
            result = result.Replace(' ', '-');
            while (result.Contains("--"))
            {
                result = result.Replace("--", "-");
            }
            
            resultViewModel.Url= result;

            var endPartTitle = "stellencha.os";
            resultViewModel.Title = anzeigen.Title + "|" + anzeigen.Company + "|" + string.Join(", ", anzeigen.Region) + "| " + endPartTitle;

            return resultViewModel;
        }
        
        private string ReplaceInvalidCharacters(string inputString)
        {
            Regex regex = new Regex(pattern);
            string result = regex.Replace(inputString, "-");
            return result;
        }
        

    }
}
