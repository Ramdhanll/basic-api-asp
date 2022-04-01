using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Models
{
   [Table("Account")]
   public class Account
   {
      [Key]
      public string NIK { get; set; }
      public string Password { get; set; }
      public int OTP { get; set; }
      public DateTime ExpiredToken { get; set; }
      public Boolean isUsed { get; set; }
      public virtual Employee Employee { get; set; }
      public virtual Profiling Profiling { get; set; }

      [JsonIgnore]
      public virtual ICollection<AccountRole> AccountRoles { get; set; }
   }
}