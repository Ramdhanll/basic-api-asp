using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Models
{
   [Table("University")]
   public class University
   {
      [Key, Required]
      public int ID { get; set; }
      [Required]
      public string Name { get; set; }
      [JsonIgnore]
      public virtual ICollection<Education> Educations { get; set; }
   }
}