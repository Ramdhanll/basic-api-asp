using api.Models;
using api.Repository.Data;
using api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
   {
      private readonly EmployeeRepository employeeRepository;

      public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
      {
         this.employeeRepository = employeeRepository;
      }

      [HttpGet("test-cors")]
      public ActionResult TestCors()
      {
         return Ok("Test Cors Berhasil!");
      }

      // [HttpGet("{nik}")]
      // public ActionResult Get<Entity>(string nik)
      // {
      //    try
      //    {
      //       var data = employeeRepository.Get(nik);
      //       if (data != null) return Ok(ResponseAPI.Response(200, "Data Ditemukan!", data));
      //       return NotFound(ResponseAPI.Response(404, $"Data employee dengan NIK {nik} tidak ditemukan"));
      //    }
      //    catch (Exception e)
      //    {
      //       return StatusCode(500, ResponseAPI.Response(404, e.Message));
      //    }
      // }

      // [HttpGet]
      // public ActionResult Get<Entity>()
      // {
      //    var data = employeeRepository.Get().Count();
      //    if (data != 0)
      //    {
      //       var success = employeeRepository.Get();
      //       return Ok(new { status = 200, success });
      //    }
      //    else
      //    {
      //       return BadRequest(new { status = 400, message = "Data is empty" });
      //    }
      // }

      // [HttpGet]
      // public override ActionResult Get<Entity>()
      // {
      //    var data = employeeRepository.Get();
      //    var count = data.ToList().Count;

      //    try
      //    {
      //       if (count > 0) return Ok(ResponseAPI.Response(200, "Data Ditemukan!", data));
      //       return Ok(ResponseAPI.Response(200, "Data masih kosong!", data));
      //    }
      //    catch (Exception)
      //    {
      //       return StatusCode(500, ResponseAPI.Response(500, "Terjadi Kesalahan"));
      //    }
      // }

      // [HttpPost("insert")]
      // public ActionResult PostEmployee(Employee employee)
      // {
      //    try
      //    {
      //       var result = employeeRepository.Insert2(employee);
      //       if (result != null) return Ok(ResponseAPI.Response(200, "Data berhasil dimasukan!", result));
      //       return BadRequest(ResponseAPI.Response(400, "Data tidak berhasil dimasukan!"));
      //    }
      //    catch (Exception e)
      //    {
      //       return BadRequest(ResponseAPI.Response(400, e.Message));
      //    }
      // }

      // [HttpPut]
      // public ActionResult Update(Employee employee)
      // {
      //    try
      //    {
      //       var result = employeeRepository.Update(employee);
      //       if (result != null) return Ok(ResponseAPI.Response(200, "Data berhasil diubah!", result));
      //       return NotFound(ResponseAPI.Response(404, $"Data employee dengan NIK {employee.NIK} tidak ditemukan!"));
      //    }
      //    catch (Exception e)
      //    {
      //       return BadRequest(ResponseAPI.Response(400, e.Message));
      //    }
      // }

      // [HttpDelete("{nik}")]
      // public ActionResult Delete(string nik)
      // {
      //    try
      //    {
      //       var result = employeeRepository.Delete(nik);
      //       return Ok(ResponseAPI.Response(200, "Data berhasil dihapus!", result));
      //    }
      //    catch (Exception e)
      //    {
      //       return BadRequest(ResponseAPI.Response(400, e.Message));
      //    }
      // }
   }
}
