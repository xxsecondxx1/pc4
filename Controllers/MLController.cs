using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.Extensions.ML;
using SentimentAnalysis;

namespace pc4.Controllers
{

    public class MLController : Controller
    {
        private readonly ILogger<MLController> _logger;
        private readonly PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> _predictionEnginePool;


        public MLController(ILogger<MLController> logger,
            PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Predict(String sentimiento)
        {
            MLModel1.ModelInput modelInput = new MLModel1.ModelInput()
            {
                SentimentText = sentimiento
            };

            MLModel1.ModelOutput prediction = _predictionEnginePool.Predict(modelInput);
            string sentiment = prediction.PredictedLabel == 0 ? "negativo" : "positivo";
            ViewData["Sentimiento"] =  sentiment;
            ViewData["Score"] =  prediction.Score[1];
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}