using api.Models;
using System;
using System.Collections.Generic;

namespace api.Utils
{
   public class ResponseAPI
   {
      public static object Response(int status, string message = "", List<object> result = null)
      {
         return new { Status = status, Result = result, Message = message, };
      }

      public static object Response(int status, string message, IEnumerable<Employee> result)
      {
         return new { Status = status, Result = result, Message = message, };
      }

      public static object Response(int status, string message, Employee result)
      {
         return new { Status = status, Result = result, Message = message, };
      }

      internal static object Response<Entity>(int status, string message, IEnumerable<Entity> result) where Entity : class
      {
         return new { Status = status, Result = result, Message = message, };

      }

      internal static object Response<Entity>(int v1, string v2, Entity result) where Entity : class
      {
         return new { Status = v1, Result = result, Message = v2, };
      }

      internal static object ResponseToken<Entity>(int v1, string v2, Entity result) where Entity : class
      {
         return new { Status = v1, Token = result, Message = v2, };
      }

      internal static object ResponseToken(int v1, string v2)
      {
         return new { Status = v1, Token = "", Message = v2, };
      }
   }
}
