using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using client.Models;

namespace client.Controllers
{
    public class LatihanController : Controller
    {
        private readonly ILogger<LatihanController> _logger;

        public LatihanController(ILogger<LatihanController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Yupee()
        {
            return View();
        }
    }
}
