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
        private static SelfCheck _selfCheck;
        private string[] _precautions;
        private string[] _descriptions;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _precautions = System.IO.File.ReadAllLines("symptom_precaution.csv");
            _descriptions = System.IO.File.ReadAllLines("symptom_Description.csv");
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
            var cStr = selfCheck.Symptoms.Split(",");
            for (int i = 0; i < col.Length; i++)
            {
                if (i < cStr.Length)
                    col[i] = cStr[i];
                else
                    col[i] = "";
            }
            
            HealthCheck.ModelInput input = new HealthCheck.ModelInput()
            {
                Col1 = col[0],
                Col2 = col[1],
                Col3 = col[2],
                Col4 = col[3],
                Col5 = col[4],
                Col6 = col[5],
                Col7 = col[6],
                Col8 = col[7],
                Col9 = col[8],
                Col10 = col[9],
                Col11 = col[10],
                Col12 = col[11],
                Col13 = col[12],
                Col14 = col[13],
                Col15 = col[14],
                Col16 = col[15],
                Col17 = col[16],
            };

            var predictionResult = HealthCheck.Predict(input).Prediction;
            //var p = HealthCheck.MultiPredict(input);

            var description = _descriptions.Where(x => x.StartsWith(predictionResult,StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var precautions = _precautions.Where(x => x.StartsWith(predictionResult, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            selfCheck.Symptoms = selfCheck.Symptoms.ToLower();
            selfCheck.Illness = predictionResult; selfCheck.Description = description;
            selfCheck.Precaution = precautions.Replace(predictionResult + ",","").Replace(",",", ");
            _selfCheck = selfCheck;

            return RedirectToAction(nameof(Results));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}