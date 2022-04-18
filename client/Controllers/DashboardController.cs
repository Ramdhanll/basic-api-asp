using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace client.Controllers
{
   public class DashboardController : Controller
   {
      private readonly ILogger<DashboardController> _logger;

      public DashboardController(ILogger<DashboardController> logger)
      {
         _logger = logger;
      }

      [Authorize(Roles = "Director, Manager, Employee")]
      public IActionResult Index()
      {

         // Decode JWT 
         var JWToken = HttpContext.Session.GetString("JWToken");
         var handler = new JwtSecurityTokenHandler();
         var token = handler.ReadJwtToken(JWToken);

         var roles = token.Claims.First(claim => claim.Type == "roles").Value;
         var email = token.Claims.First(claim => claim.Type == "email").Value;


         Console.WriteLine("Decode JWT Roles => {0}, Email => {1}", roles, email);

         return View();
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
