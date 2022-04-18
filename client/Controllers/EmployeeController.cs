using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using client.Models;
using Microsoft.AspNetCore.Http;
using client.Base;
using api.Models;
using client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;

namespace client.Controllers
{
   public class EmployeeController : BaseController<Employee, EmployeeRepository, string>
   {

      private readonly EmployeeRepository repository;

      public EmployeeController(EmployeeRepository repository) : base(repository)
      {
         this.repository = repository;
      }

      [Authorize(Roles = "Director, Manager")]
      public IActionResult Index()
      {
         var JWToken = HttpContext.Session.GetString("JWToken");
         ViewBag.Token = JWToken;

         return View();
      }

      public IActionResult Yupee()
      {
         return View();
      }
   }
}
