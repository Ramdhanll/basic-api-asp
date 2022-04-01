using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Utils
{
   public class Converts
   {
      public static string CovertGender(string gender)
      {
         string result = "";
         switch (gender)
         {
            case "Male":
               return result = "Laki-laki";
            case "Female":
               return result = "Perempuan";
            default:
               break;
         }
         return result;
      }
   }
}