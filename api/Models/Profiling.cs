using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
   [Table("Profiling")]
   public class Profiling
   {
      [Key]
      public string NIK { get; set; }
      public int EducationId { get; set; }
      public virtual Account Account { get; set; }
      public virtual Education Education { get; set; }
   }
}