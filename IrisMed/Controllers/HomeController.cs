using IrisMed.Data;
using IrisMed.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HealthCheck_ConsoleApp;

namespace IrisMed.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static SelfCheck[] _selfCheck;
        private string[] _precautions;
        private string[] _descriptions;
        private string[] _dataset;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _precautions = System.IO.File.ReadAllLines("symptom_precaution.csv");
            _descriptions = System.IO.File.ReadAllLines("symptom_Description.csv");
            _dataset = System.IO.File.ReadAllLines("dataset.csv");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactUS()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SelfCheck()
        {
            return View();
        }

        public IActionResult Results()
        {
            return View(_selfCheck);
        }


        [HttpPost]
        public IActionResult SelfCheck([Bind("Symptoms")] SelfCheck selfCheck)
        {
            string[] col = new string[17];
            var cStr = selfCheck.Symptoms.Replace(" ","").Trim(',').Split(",");
            for (int i = 0; i < col.Length; i++)
            {
                if (i < cStr.Length)
                    col[i] = cStr[i].Replace(" ","_");
                else
                    col[i] = "";
            }
            var result = HealthCheck.SecondPrediction(cStr, _dataset);

            //var p = HealthCheck.MultiPredict(input);

            _selfCheck = new SelfCheck[result.Length];

            if (result[0] != "")
            {
                for (int i = 0; i < result.Length; i++)
                {
                    var predictionResult = result[i];

                    var description = _descriptions.Where(x => x.StartsWith(predictionResult, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    var precautions = _precautions.Where(x => x.StartsWith(predictionResult, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    _selfCheck[i] = new SelfCheck();
                    _selfCheck[i].Symptoms = selfCheck.Symptoms.ToLower();
                    _selfCheck[i].Illness = predictionResult; _selfCheck[i].Description = description;
                    _selfCheck[i].Precaution = precautions.Replace(predictionResult + ",", "").Replace(",", ", ");
                }
            }
            


            return RedirectToAction(nameof(Results));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}