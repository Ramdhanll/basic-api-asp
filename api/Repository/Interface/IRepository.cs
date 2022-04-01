using System.Collections.Generic;
using api.Models;
using api.Utils;

namespace basic_api.Repository.Interface
{
   public interface IRepository<Entity, Key> where Entity : class
   {
      IEnumerable<Entity> Get();
      Entity Get(Key key);
      Entity Insert(Entity entity);
      Entity Update(Entity entity);
      Result Delete(Key key);
   }
}
