using Newtonsoft.Json;
using System.Text.Json;
using Test01;
using Test01.Model;

public class Program
{
    
    public static void Main(string[] args)
    {
        string jsonData = @"[
                            {
                            'id':12345,
                            'title':'Junior Software Developer (m/w/d) - 100% remote',
                            'company':'Software & More Inc.',
                            'region':['München']

                            },
                            {
                            'id':23456,
                            'title':'Software Developer (m/w/d) - 100% Home-Office möglich!',
                            'company':'Professional Software GmbH & Co.KG',
                            'region':['München', 'Bamberg', 'Augsburg']
                            },
                            {
                            'id':34567,
                            'title':'Senior Software Developer (m/w/d) - 50% Außendienst',
                            'company':'Daten & Lösungen AG',
                            'region':['Berlin', 'Hamburg', 'Frankfurt', 'Köln', 'Bayern']
                            }]";

        //var lstAnzeigens = JsonSerializer.Deserialize<List<Anzeigen>>(jsonData);
        var lstAnzeigens = JsonConvert.DeserializeObject<List<Anzeigen>>(jsonData); ;
        
        CreateResult createResult = new CreateResult();


        var lstResultViewModel = createResult.GetAnzeigens(lstAnzeigens);

        foreach (var resultViewModel in lstResultViewModel)
        {
            Console.WriteLine("URL  " + resultViewModel.Url);
            Console.WriteLine("Title  " + resultViewModel.Title);
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.WriteLine("*************** Part2 ********************");
        Console.WriteLine();


        CalculateURLAndTitle("stellencha.os", lstAnzeigens);
        CalculateURLAndTitle("jobs-mit.biz", lstAnzeigens);
        CalculateURLAndTitle("job.dealer", lstAnzeigens);
    }

    public static void CalculateURLAndTitle(string portal, List<Anzeigen> lstAnzeigens)
    {
        CreateResultPart02 createResultPart02 = new CreateResultPart02();
        foreach (var anzeigen in lstAnzeigens)
        {
            string url = "";
            string pageTitle = "";

            switch (portal)
            {
                case "stellencha.os":
                    url = createResultPart02.GenerateStellenchaOSURL(anzeigen);
                    pageTitle = createResultPart02.GenerateStellenchaOSTitle(anzeigen);
                    break;
                case "jobs-mit.biz":
                    url = createResultPart02.GenerateJobsMitBizURL(anzeigen);
                    pageTitle = createResultPart02.GenerateJobsMitBizTitle(anzeigen);
                    break;
                case "job.dealer":
                    url = createResultPart02.GenerateJobDealerURL(anzeigen);
                    pageTitle = createResultPart02.GenerateJobDealerTitle(anzeigen);
                    break;
                default:
                    Console.WriteLine("Invalid portal.");
                    return;
            }

            Console.WriteLine("{0} - Job posting {1}", portal.ToUpper(), anzeigen.Id);
            Console.WriteLine("URL: {0}", url);
            Console.WriteLine("Title: {0}", pageTitle);
            Console.WriteLine();
        }
    }
}


