using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
   [Table("Education")]
   public class Education
   {
      [Key]
      public int Id { get; set; }
      public string Degree { get; set; }
      public string GPA { get; set; }
      public int UniversityId { get; set; }
      public virtual ICollection<Profiling> Profilings { get; set; }
      public virtual University University { get; set; }
   }
}