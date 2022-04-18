using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Models
{
   [Table("Role")]
   public class Role
   {
      [Key]
      public int ID { get; set; }
      public string Name { get; set; }
      [JsonIgnore]
      public virtual ICollection<AccountRole> AccountRoles { get; set; }
   }
}