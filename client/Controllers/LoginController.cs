using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using client.Models;
using client.Base;
using api.ViewModel;
using client.Repositories.Data;
using Microsoft.AspNetCore.Http;

namespace client.Controllers
{
   public class LoginController : BaseController<LoginVM, LoginRepository, string>
   {
      private readonly LoginRepository repository;

      public LoginController(LoginRepository repository) : base(repository)
      {
         this.repository = repository;
      }


      [HttpPost]
      public async Task<IActionResult> Auth(LoginVM loginVM)
      {
         var jwtToken = await repository.Auth(loginVM);

         var token = jwtToken.token;

         if (token == "")
         {
            ViewBag.Message = jwtToken.message;
            return View("index");
         }

         HttpContext.Session.SetString("JWToken", token);

         return RedirectToAction("index", "dashboard");
      }


      public IActionResult Index()
      {
         var JWToken = HttpContext.Session.GetString("JWToken");
         if (JWToken != null) return RedirectToAction("index", "dashboard");

         return View();
      }

      public IActionResult Logout()
      {
         HttpContext.Session.Remove("JWToken");
         return RedirectToAction("index", "Login");
      }
   }
}
