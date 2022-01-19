using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectWorld.Graphics.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Err403()
        {
            return View();
        }
        public IActionResult Err404()
        {
            return View();
        }
        public IActionResult Err500()
        {
            return View();
        }
    }
}
