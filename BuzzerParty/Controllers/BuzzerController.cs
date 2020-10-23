using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BuzzerParty.Models;

namespace BuzzerParty.Controllers
{
    public class BuzzerController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public BuzzerController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            Models.IndexViewModel indexViewModel = new Models.IndexViewModel()
            {
                AppName = "BuzzerParty"
            };
            return View(indexViewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}