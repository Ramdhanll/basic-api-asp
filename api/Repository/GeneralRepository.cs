using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Context;
using api.Models;
using api.Utils;
using basic_api.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace basic_api.Repository
{
   public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
      where Entity : class
      where Context : MyContext
   {
      private Result result = new Result
      {
         Status = 0
      };
      private readonly MyContext context;
      private readonly DbSet<Entity> entities;

      public GeneralRepository(MyContext context)
      {
         this.context = context;
         entities = context.Set<Entity>();
      }

      public Result Delete(Key key)
      {
         var entity = entities.Find(key);
         if (entity == null)
         {
            result.Status = 1;
            return result;
         }

         entities.Remove(entity);
         context.SaveChanges();

         result.Status = 200;
         result.Data = entity;

         return result;
      }

      public IEnumerable<Entity> Get()
      {
         return entities.ToList();
      }

      public Entity Get(Key key)
      {
         return entities.Find(key);
      }

      public Entity Insert(Entity entity)
      {
         if (entity == null)
            throw new ArgumentNullException("entity");
         entities.Add(entity);
         context.SaveChanges();
         return entity;
      }

      public Entity Update(Entity entity)
      {
         if (entity == null)
            throw new ArgumentNullException("entity");
         context.Entry(entity).State = EntityState.Modified;
         var result = context.SaveChanges();
         if (result == 1)
            return entity;
         throw new Exception("Data tidak berhasil diubah!");
      }
   }
}