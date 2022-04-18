using api.Context;
using api.Models;
using api.Utils;
using api.ViewModel;
using basic_api.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.Repository.Data
{
   public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
   {
      private readonly MyContext context;
      private Result result = new Result
      {
         Status = 0
      };

      public AccountRoleRepository(MyContext context) : base(context)
      {
         this.context = context;
      }

      public Result AssignManager(string email)
      {
         // deklarasi var roles
         var roles = new List<string>();

         // get data employee join berdasarkan email
         var data = (
            from employee in context.Employees
            join account in context.Accounts
               on employee.NIK equals account.NIK
            join accountRole in context.AccountRoles
               on account.NIK equals accountRole.NIK
            join role in context.Roles
               on accountRole.RoleID equals role.ID
            where employee.Email == email
            select new
            {
               NIK = employee.NIK,
               roles = role.Name
            }
         ).ToList();

         // validasi jika akun tidak ada
         if (isError(data.Count() == 0, 1)) return result;

         // masukan data roles ke dalam var roles
         foreach (var item in data)
         {
            roles.Add(item.roles);
         }

         // validasi jika user sudah menjadi manager
         if (isError(roles.Contains("Manager"), 2)) return result;

         // validasi jika role manager belum ada
         var roleManager = context.Roles.FirstOrDefault(r => r.Name == "Manager");
         if (isError(roleManager == null, 3)) return result;

         var createAccountRole = new AccountRole
         {
            NIK = data[0].NIK,
            RoleID = roleManager.ID
         };

         context.AccountRoles.Add(createAccountRole);
         context.SaveChanges();

         result.Status = 200;
         return result;
      }

      private bool isError(bool condition, int status)
      {
         if (condition)
         {
            result.Status = status;
            return true;
         }

         return false;
      }
   }
}
