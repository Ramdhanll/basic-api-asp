using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Models
{
   [Table("Profiling")]
   public class Profiling
   {
      [Key, Required]
      public string NIK { get; set; }
      [Required]
      public int EducationId { get; set; }
      [JsonIgnore]
      public virtual Account Account { get; set; }
      [JsonIgnore]
      public virtual Education Education { get; set; }
   }
}