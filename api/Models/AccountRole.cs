using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Models
{
   [Table("AccountRole")]
   public class AccountRole
   {
      [Key]
      public int Id { get; set; }
      public string AccountId { get; set; }
      public int RoleId { get; set; }

      [JsonIgnore]
      public virtual Account Account { get; set; }
      public virtual Role Role { get; set; }
   }
}