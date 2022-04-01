using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class JwtController : ControllerBase
   {
      [Authorize]
      [HttpGet("test")]
      public ActionResult TestJWT()
      {
         return Ok("Test JWT berhasil!");
      }
   }
}