using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.ViewModel
{
   public class UpdateVM
   {
      // Employee Model
      public string NIK { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Phone { get; set; }
      public DateTime BirthDate { get; set; }
      public Gender Gender { get; set; }
      public int Salary { get; set; }
      public string Email { get; set; }

      // Education Model
      public int Education_id { get; set; }
      public string Degree { get; set; }
      public string GPA { get; set; }
      public int UniversityId { get; set; }
   }
}