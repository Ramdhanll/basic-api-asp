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
using api.ViewModel;

namespace client.Controllers
{
   public class AccountController : BaseController<Account, AccountRepository, string>
   {
      private readonly AccountRepository repository;

      public AccountController(AccountRepository repository) : base(repository)
      {
         this.repository = repository;
      }

      // [HttpGet]
      // public async Task<JsonResult> GetMasterByNIK(string nik)
      // {
      //    Console.WriteLine(nik);
      //    var result = await repository.GetMasterByNIK(nik);
      //    return Json(nik);
      // }

      [HttpGet]
      public async Task<JsonResult> GetMaster()
      {
         var result = await repository.GetMaster();
         return Json(result);
      }

      [HttpPost]
      public async Task<JsonResult> Register([FromBody] RegisterVM registerVM)
      {
         var result = await repository.Register(registerVM);
         return Json(result);
      }
   }
}
