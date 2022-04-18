using client.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using client.Base;
using api.Models;
using Microsoft.AspNetCore.Authorization;

namespace client.Controllers
{
   public class UniversityController : BaseController<University, UniversityRepository, int>
   {
      private readonly UniversityRepository repository;

      public UniversityController(UniversityRepository repository) : base(repository)
      {
         this.repository = repository;
      }

      [HttpGet]
      public async Task<JsonResult> GetUniversities()
      {
         var result = await repository.GetUniversities();
         return Json(result);
      }

      [Authorize(Roles = "Director, Manager")]
      public IActionResult Index()
      {
         return View();
      }
   }
}
