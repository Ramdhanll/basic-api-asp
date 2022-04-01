using api.Controllers;
using api.Models;
using api.Repository.Data;
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
   public class EducationsController : BaseController<Education, EducationRepository, int>
   {
      public EducationsController(EducationRepository repository) : base(repository)
      {

      }
   }
}