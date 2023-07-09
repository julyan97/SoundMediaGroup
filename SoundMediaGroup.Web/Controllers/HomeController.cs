using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Models.HomeModels;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IResourceService _resourceService;

        public HomeController(
            ILogger<HomeController> logger,
            IResourceService resourceService)
        {
            _logger = logger;
            _resourceService = resourceService;
        }

        public IActionResult Index()
        {
           var outputModel = _resourceService.GetHomeInfo();
            return View(outputModel);
        }

        public IActionResult Contacts()
        {
            var outputModel = _resourceService.GetContactsInfo();
            return View(outputModel);
        }

        public IActionResult Bio()
        {
            var outputModel = _resourceService.GetBioInfo();
            return View(outputModel);
        }

        public IActionResult Portfolio()
        {
            var outputModel = _resourceService.GetPortfolioInfo();
            return View(outputModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}