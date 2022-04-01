using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.ViewModel
{
   public class LoginVM
   {
      public string Email { get; set; }

      // Account Model
      public string Password { get; set; }
   }
}