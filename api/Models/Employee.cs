using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace api.Models
{

   [Table("Employee")]
   public class Employee
   {
      // membuat table dengan metode code first
      [Key]
      public string NIK { get; set; }
      [Required]
      public string FirstName { get; set; }
      [Required]
      public string LastName { get; set; }
      [Required]
      public string Phone { get; set; }
      [Required]
      public DateTime BirthDate { get; set; }
      [Required]
      public int Salary { get; set; }
      [Required]
      public string Email { get; set; }
      [Required]
      public Gender Gender { get; set; }
      [JsonIgnore]
      public virtual Account Account { get; set; }
   }

   public enum Gender
   {
      Male = 0,
      Female = 1
   }
   /**
    * 
    * File context = jembatan antara aplikasi dengan database
    * menggunakan ORM entity framework
    */


   // update-database
}
