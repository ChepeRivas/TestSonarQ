using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfectWorld.Data.Dtos;
using PerfectWorld.Data.Interfaces;
using PerfectWorld.Graphics.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectWorld.Graphics.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IWebUserHelper _userHelper;
        public HomeController(ILogger<HomeController> logger, IWebUserHelper userHelper)
        {
            _logger = logger;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            var account = new VMUsers() { Usuario = _userHelper.GetUserLogin() };  
            return View(account);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
