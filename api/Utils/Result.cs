using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Utils
{
   public class Result
   {
      public int Status { get; set; }
      /**
      Status
      0 => Default
      1 => validasi pertama
      2 => validasi kedua
      ....
      200 => valid
      **/
      public object Data { get; set; }


      public bool checkIsValidationError(int status)
      {
         Status = status;
         return Status != 0 ? true : false;
      }
   }
}