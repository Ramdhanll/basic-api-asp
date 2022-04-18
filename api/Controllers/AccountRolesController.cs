using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Repository.Data;
using api.Utils;
using api.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
   {
      private readonly AccountRoleRepository accountRoleRepository;

      public AccountRolesController(AccountRoleRepository repository) : base(repository)
      {
         this.accountRoleRepository = repository;
      }

      [HttpPost("sign-manager")]
      [Authorize(Roles = "Director")]
      public ActionResult AssignManager(AssignManagerVM assignManagerVM)
      {
         try
         {
            var result = accountRoleRepository.AssignManager(assignManagerVM.Email);
            return result.Status switch
            {
               200 => Ok(ResponseAPI.Response(200, "Assign manager berhasil!", result.Data)),
               1 => BadRequest(ResponseAPI.Response(400, "Akun tidak ditemukan!")),
               2 => BadRequest(ResponseAPI.Response(400, "Role sudah manager!")),
               3 => BadRequest(ResponseAPI.Response(400, "Role manager belum ada, tambahkan terlebih dahulu!")),
               _ => BadRequest(ResponseAPI.Response(400, "Assign manager gagal!"))
            };
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }
   }
}