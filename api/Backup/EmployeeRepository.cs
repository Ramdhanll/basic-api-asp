using api.Context;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.Repository
{
   public class EmployeeRepository : IEmployeeRepository
   {
      private readonly MyContext context;

      public EmployeeRepository(MyContext context)
      {
         this.context = context;
      }

      public IEnumerable<Employee> Get()
      {
         var result = context.Employees.ToList();
         return result;
      }

      public Employee Get(string NIK)
      {
         var employee = context.Employees.Find(NIK);
         return employee;
      }

      public Employee Insert(Employee employee)
      {
         var isEmployee = context.Employees.Find(employee.NIK);
         if (isEmployee != null) throw new Exception("Data employee sudah ada!");

         employee.NIK = generatedNIK();

         var checkPhone = context.Employees.Any(e => e.Phone == employee.Phone);
         if (checkPhone) throw new Exception("Nomor telepon sudah digunakan!");

         var checkEmail = context.Employees.Any(e => e.Email == employee.Email);
         if (checkEmail) throw new Exception("Email sudah digunakan!");

         context.Employees.Add(employee);

         context.SaveChanges();
         return employee;

         // var employees = context.Employees.ToList();

         // var isExist = employees.Find(e => e.NIK == employee.NIK);
         // if (isExist != null) throw new Exception("Data employee sudah ada!");

         // employee.NIK = generatedNIK();

         // var checkPhone = employees.Any(e => e.Phone == employee.Phone);
         // if (checkPhone) throw new Exception("Nomor telepon sudah digunakan!");

         // var checkEmail = employees.Any(e => e.Email == employee.Email);
         // if (checkEmail) throw new Exception("Email sudah digunakan!");

         // context.Employees.Add(employee);

         // context.SaveChanges();
         // return employee;
      }

      public Employee Update(Employee employee)
      {
         var result = context.Employees.SingleOrDefault(e => e.NIK == employee.NIK);
         if (result != null)
         {
            ValidationCheckForUpdate(employee, "Phone");
            ValidationCheckForUpdate(employee, "Email");

            result.FirstName = employee.FirstName;
            result.LastName = employee.LastName;
            result.Phone = employee.Phone;
            result.BirthDate = employee.BirthDate;
            result.Salary = employee.Salary;
            result.Email = employee.Email;

            context.SaveChanges();
            return result;
         }

         return result;
      }

      private void ValidationCheckForUpdate(Employee employee, string field)
      {
         if (field == "Phone")
         {
            var checkPhone = context.Employees.Any(e => e.Phone == employee.Phone && e.NIK != employee.NIK);
            if (checkPhone) throw new Exception("Nomor telepon sudah digunakan!");
         }
         else if (field == "Email")
         {
            var checkEmail = context.Employees.Any(e => e.Email == employee.Email && e.NIK != employee.NIK);
            if (checkEmail) throw new Exception("Email sudah digunakan!");
         }
      }

      public Employee Delete(string NIK)
      {
         var employee = context.Employees.Find(NIK);
         if (employee == null) throw new Exception("Data employee tidak ditemukan!");

         context.Employees.Remove(employee);

         var isSave = context.SaveChanges();
         if (isSave == 1) return employee;

         throw new Exception("Data employee tidak berhasil dihapus!");
      }

      private string generatedNIK()
      {
         var yearNow = DateTime.Now.ToString("yyyy");

         // var employeeLast = context.Employees.LastOrDefault().NIK; // Get NIK tertinggi
         var maxNIK = context.Employees.ToList().Max(e => e.NIK); // Get NIK tertinggi

         if (maxNIK == null) return yearNow + "001";

         // Validasi jika tahun berganti maka dibuat menjadi 001 lagi.
         var yearEmployeeCreated = maxNIK.Substring(0, 4);

         if (yearEmployeeCreated == yearNow)
         {
            var identityNIK = Int32.Parse(maxNIK.Substring(6)) + 1;
            return yearNow + "00" + identityNIK.ToString();
         }
         else
         {
            return yearNow + "001";
         }
      }


   }
}
