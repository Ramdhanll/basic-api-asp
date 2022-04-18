using System;
using System.Linq;
using api.Models;
using api.Utils;
using basic_api.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
   public class BaseController<Entity, Repository, Key> : ControllerBase
   where Entity : class
   where Repository : IRepository<Entity, Key>
   {
      private readonly Repository repository;

      public BaseController(Repository repository)
      {
         this.repository = repository;
      }

      [HttpGet]
      public ActionResult<Entity> Get()
      {
         var data = repository.Get();
         var count = data.ToList().Count;

         try
         {
            if (count > 0) return Ok(ResponseAPI.Response(200, "Data Ditemukan!", data));
            return Ok(ResponseAPI.Response(200, "Data masih kosong!", data));
         }
         catch (Exception)
         {
            return StatusCode(500, ResponseAPI.Response(500, "Terjadi Kesalahan"));
         }
      }

      [HttpGet("{key}")]
      public ActionResult Get(Key key)
      {
         try
         {
            var data = repository.Get(key);
            if (data != null) return Ok(ResponseAPI.Response(200, "Data Ditemukan!", data));
            return NotFound(ResponseAPI.Response(404, $"Data tidak ditemukan!"));
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(404, e.Message));
         }
      }

      [HttpPost]
      public ActionResult Post(Entity entity)
      {
         try
         {
            var result = repository.Insert(entity);
            if (result != null) return Ok(ResponseAPI.Response(200, "Data berhasil dimasukan!", result));
            return BadRequest(ResponseAPI.Response(400, "Data tidak berhasil dimasukan!"));
         }
         catch (Exception e)
         {
            return BadRequest(ResponseAPI.Response(400, e.Message));
         }
      }

      [Authorize(Roles = "Director, Manager")]
      [HttpDelete("{key}")]
      public ActionResult Delete(Key key)
      {
         try
         {
            var result = repository.Delete(key);
            return result.Status switch
            {
               200 => Ok(ResponseAPI.Response(200, "Data berhasil dihapus!", result.Data)),
               1 => BadRequest(ResponseAPI.Response(400, "Data tidak dapat ditemukan!")),
               _ => BadRequest(ResponseAPI.Response(400, "Data gagal dihapus!"))
            };
         }
         catch (Exception e)
         {
            return StatusCode(500, ResponseAPI.Response(500, e.Message));
         }
      }

      [HttpPut]
      public ActionResult Update(Entity entity)
      {
         try
         {
            var result = repository.Update(entity);
            if (result != null) return Ok(ResponseAPI.Response(200, "Data berhasil diubah!", result));
            return BadRequest(ResponseAPI.Response(400, "Data tidak berhasil dimasukan!"));
         }
         catch (Exception e)
         {
            return BadRequest(ResponseAPI.Response(400, e.Message));
         }
      }
   }
}