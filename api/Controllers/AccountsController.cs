using api.Controllers;
using api.Models;
using api.Repository.Data;
using api.Utils;
using api.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AccountsController : BaseController<Account, AccountRepository, string>
   {
      private readonly AccountRepository accountRepository;
      private IConfiguration _config;
      public AccountsController(IConfiguration config, AccountRepository repository) : base(repository)
      {
         this._config = config;
         this.accountRepository = repository;
      }

      [HttpGet("master")]
      [Authorize(Roles = "Director, Manager, Employee")]
      public ActionResult GetMasterEmployeeData()
      {
         try
         {
            var result = accountRepository.GetMasterEmployeeData();
            if (result == null) return NotFound(ResponseAPI.Response(404, "Data masih kosong!", result));
            return Ok(ResponseAPI.Response(200, "Data berhasil didapatkan!", result));
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }

      [HttpGet("master/{nik}")]
      // [Authorize(Roles = "Director, Manager")]
      public ActionResult GetMasterEmployeeDataByNIK(string nik)
      {
         try
         {
            var result = accountRepository.GetMasterEmployeeDataByNIK(nik);

            switch (result.Status)
            {
               case 1:
                  return BadRequest(ResponseAPI.Response(400, "Akun tidak dapat ditemukan!"));
               case 200:
                  return Ok(ResponseAPI.Response(200, "Data berhasil didapatkan!", result.Data));
               default:
                  return BadRequest(ResponseAPI.Response(400, "Login gagal!"));
            }
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }

      [HttpPost("login")]
      public ActionResult Login(LoginVM loginVM)
      {
         try
         {
            var result = accountRepository.Login(loginVM);

            switch (result.Status)
            {
               case 1:
                  return BadRequest(ResponseAPI.ResponseToken(400, "Email tidak dapat ditemukan!"));
               case 2:
                  return BadRequest(ResponseAPI.ResponseToken(400, "Password salah!"));
               case 200:
                  var roles = accountRepository.GetRolesByEmail(loginVM.Email);
                  var token = GenerateToken(loginVM.Email, roles);
                  return Ok(ResponseAPI.ResponseToken(200, "Berhasil login!", token));
               default:
                  return BadRequest(ResponseAPI.ResponseToken(400, "Login gagal!"));
            }
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }

      private string GenerateToken(string email, List<string> roles)
      {
         var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
         var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

         var claims = new List<Claim>();
         claims.Add(new Claim("email", email));

         foreach (var item in roles)
         {
            claims.Add(new Claim("roles", item));
         }

         var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
         );

         return new JwtSecurityTokenHandler().WriteToken(token);
      }

      [Authorize(Roles = "Director, Manager")]
      [HttpPost("register")]
      public ActionResult Register(RegisterVM registerVM)
      {
         try
         {
            var result = accountRepository.Register(registerVM);
            return result.Status switch
            {
               200 => Ok(ResponseAPI.Response(200, "Registrasi berhasil!", result.Data)),
               1 => BadRequest(ResponseAPI.Response(400, "Nomor telepon sudah digunakan!")),
               2 => BadRequest(ResponseAPI.Response(400, "Email sudah digunakan!")),
               3 => NotFound(ResponseAPI.Response(404, "Role employee tidak ada, silahkan tambah role employee terlebih dahulu!")),
               _ => BadRequest(ResponseAPI.Response(400, "Registrasi gagal!"))
            };
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }

      [Authorize(Roles = "Director, Manager")]
      [HttpPut("master/update")]
      public ActionResult UpdateAccount(UpdateVM updataVM)
      {
         try
         {
            var result = accountRepository.UpdateAccount(updataVM);
            return result.Status switch
            {
               200 => Ok(ResponseAPI.Response(200, "Update berhasil!", result.Data)),
               1 => BadRequest(ResponseAPI.Response(400, "Nomor telepon sudah digunakan!")),
               2 => BadRequest(ResponseAPI.Response(400, "Email sudah digunakan!")),
               3 => NotFound(ResponseAPI.Response(404, "Role employee tidak ada, silahkan tambah role employee terlebih dahulu!")),
               _ => BadRequest(ResponseAPI.Response(400, "Update gagal!"))
            };
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }

      [Authorize(Roles = "Director, Manager")]
      [HttpDelete("remove/{nik}")]
      public ActionResult DeleteAccount(string nik)
      {
         try
         {
            var result = accountRepository.DeleteAccount(nik);

            switch (result.Status)
            {
               case 1:
                  return BadRequest(ResponseAPI.Response(400, "Akun tidak dapat ditemukan!"));
               case 2:
                  return BadRequest(ResponseAPI.Response(400, "Password salah!"));
               case 200:
                  return Ok(ResponseAPI.Response(200, "Akun berhasil dihapus!"));
               default:
                  return BadRequest(ResponseAPI.Response(400, "Akun gagal dihapus!"));
            }
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }


      [HttpPost("forgot-password")]
      public ActionResult ForgotPassword(ForgotPassword forgotPassword)
      {
         try
         {
            var result = accountRepository.ForgetPassword(forgotPassword);

            return result.Status switch
            {
               200 => Ok(ResponseAPI.Response(200, "OTP berhasil dikirim, Silahkan cek email anda!", result.Data)),
               1 => NotFound(ResponseAPI.Response(404, "Akun tidak dapat ditemukan!")),
               _ => BadRequest(ResponseAPI.Response(400, "OTP gagal dikirim!"))
            };
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }

      [HttpPost("change-password")]
      public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
      {
         try
         {
            var result = accountRepository.ChangePassword(changePasswordVM);

            return result.Status switch
            {
               200 => Ok(ResponseAPI.Response(200, "Password berhasil diganti!", result.Data)),
               1 => NotFound(ResponseAPI.Response(404, "Akun tidak dapat ditemukan!")),
               2 => BadRequest(ResponseAPI.Response(400, "OTP invalid!")),
               3 => BadRequest(ResponseAPI.Response(400, "OTP sudah digunakan!")),
               4 => BadRequest(ResponseAPI.Response(400, "OTP expired, silahkan request OTP kembali!")),
               5 => BadRequest(ResponseAPI.Response(400, "Password tidak match!")),
               _ => BadRequest(ResponseAPI.Response(400, "Password gagal diganti!"))
            };
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }
   }
}