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
      [Key, Required]
      public int ID { get; set; }
      [Required]
      public string NIK { get; set; }
      [Required]
      public int RoleID { get; set; }
      [JsonIgnore]
      public virtual Account Account { get; set; }
      [JsonIgnore]
      public virtual Role Role { get; set; }
   }
}