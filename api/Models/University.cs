using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
   [Table("University")]
   public class University
   {
      [Key]
      public int Id { get; set; }
      [Required]
      public string Name { get; set; }
      public virtual ICollection<Education> Educations { get; set; }
   }
}