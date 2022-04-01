using api.Context;
using api.Models;
using basic_api.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.Repository.Data
{
   public class UniversityRepository : GeneralRepository<MyContext, University, int>
   {
      private readonly MyContext context;

      public UniversityRepository(MyContext context) : base(context)
      {
         this.context = context;
      }
   }
}
