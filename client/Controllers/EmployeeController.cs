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
   public class EmployeeController : Controller
   {
      private readonly ILogger<EmployeeController> _logger;

      public EmployeeController(ILogger<EmployeeController> logger)
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
