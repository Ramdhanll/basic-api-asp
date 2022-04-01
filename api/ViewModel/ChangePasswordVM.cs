using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.ViewModel
{
   public class ChangePasswordVM
   {
      // email, otp, newpassword, confirmpassword
      public string Email { get; set; }
      public int OTP { get; set; }
      public string NewPassword { get; set; }
      public string Confirmpassword { get; set; }

   }
}