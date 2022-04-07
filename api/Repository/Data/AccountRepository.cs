using api.Context;
using api.Models;
using api.Utils;
using api.ViewModel;
using basic_api.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using BC = BCrypt.Net.BCrypt;

namespace api.Repository.Data
{
   public class AccountRepository : GeneralRepository<MyContext, Account, string>
   {
      private Result result = new Result
      {
         Status = 0
      };

      private readonly MyContext context;

      public AccountRepository(MyContext context) : base(context)
      {
         this.context = context;
      }

      public IEnumerable GetMasterEmployeeData()
      {

         var result = (
            from employee in context.Employees
            join account in context.Accounts
               on employee.NIK equals account.NIK
            join profiling in context.Profilings
               on account.NIK equals profiling.NIK
            join education in context.Educations
               on profiling.EducationId equals education.Id
            join university in context.Universities
               on education.UniversityId equals university.Id
            select new
            {
               NIK = employee.NIK,
               FullName = employee.FirstName + " " + employee.LastName,
               employee.Phone,
               Gender = Converts.CovertGender(employee.Gender.ToString()),
               employee.Email,
               employee.BirthDate,
               employee.Salary,
               Education_id = profiling.EducationId,
               education.GPA,
               education.Degree,
               UniversityName = university.Name,
               Roles = (from account in context.AccountRoles
                        join role in context.Roles
                        on account.RoleId equals role.Id
                        where account.AccountId == employee.NIK
                        select new
                        {
                           role.Name
                        }).Select(x => x.Name).ToArray()
            }
         ).ToList();

         return result;
      }

      public Result Register(RegisterVM registerVM)
      {
         string NIK = generatedNIK();

         // ValidationPhoneOrEmailIsExist mengembalikan 1 / 2 yang berarti ada error validation
         var checkPhoneOrEmail = ValidationPhoneOrEmailIsExist(NIK, registerVM.Phone, registerVM.Email);

         // check jika ada validation error maka function berhenti dan return error
         if (result.checkIsValidationError(checkPhoneOrEmail)) return result;

         // validasi jika tidak ada role Employee
         var getRoleEmployee = context.Roles.FirstOrDefault(r => r.Name == "Employee");
         if (getRoleEmployee == null)
         {
            result.Status = 3;
            return result;
         }

         var emp = new Employee
         {
            NIK = NIK,
            FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            Email = registerVM.Email,
            Salary = registerVM.Salary,
            Phone = registerVM.Phone,
            Gender = registerVM.Gender,
            BirthDate = registerVM.BirthDate
         };

         var account = new Account
         {
            NIK = emp.NIK,
            Password = BC.HashPassword(registerVM.Password)
         };

         var education = new Education
         {
            Degree = registerVM.Degree,
            GPA = registerVM.GPA,
            UniversityId = registerVM.UniversityId
         };

         var accountRole = new AccountRole
         {
            AccountId = emp.NIK,
            RoleId = getRoleEmployee.Id
         };

         // add to model
         context.Employees.Add(emp);
         context.Accounts.Add(account);
         context.Educations.Add(education);
         context.AccountRoles.Add(accountRole);

         context.SaveChanges();

         // insert profiling
         var profiling = new Profiling
         {
            NIK = emp.NIK,
            EducationId = education.Id
         };
         context.Profilings.Add(profiling);
         context.SaveChanges();

         // add generated NIK to registerVM.NIK for display result
         registerVM.NIK = NIK;
         registerVM.Password = account.Password;

         result.Status = 200;
         result.Data = registerVM;

         return result;
      }

      public Result Login(LoginVM loginVM)
      {
         var employeeByEmail = context.Employees.FirstOrDefault(e => e.Email == loginVM.Email);
         if (employeeByEmail == null)
         {
            result.Status = 1;
            return result;
         };

         var account = context.Accounts.Find(employeeByEmail.NIK);
         if (!BC.Verify(loginVM.Password, account.Password))
         {
            result.Status = 2;
            return result;
         };

         result.Status = 200;
         return result;
      }

      public List<string> GetRolesByEmail(string email)
      {
         var result = new List<string>();
         var data = (
            from employee in context.Employees
            join account in context.Accounts
               on employee.NIK equals account.NIK
            join accountRole in context.AccountRoles
               on account.NIK equals accountRole.AccountId
            join role in context.Roles
               on accountRole.RoleId equals role.Id
            where employee.Email == email
            select new
            {
               roles = role.Name
            }
         );

         foreach (var item in data)
         {
            result.Add(item.roles);
         };

         return result;
      }

      public Result DeleteAccount(string nik)
      {
         /**
         accountRole
         profilling
         account
         employee
         **/
         var employee = context.Employees.Find(nik);
         if (employee == null)
         {
            result.Status = 1;
            return result;
         };

         return result;
      }

      public Result ForgetPassword(ForgotPassword forgotPasswordVM)
      {
         // validasi email ada atau tidak
         var employee = context.Employees.FirstOrDefault(e => e.Email == forgotPasswordVM.Email);
         if (employee == null)
         {
            result.Status = 1;
            return result;
         };

         // create OTP random 6 numbers
         var otp = new Random().Next(0, 1000000);

         // send email
         var to = employee.Email;
         var from = "erdevtesting@gmail.com";
         MailMessage message = new MailMessage(from, to);

         string mailBody = $"OTP send: {otp}";
         message.Subject = "Forgot Password";
         message.Body = mailBody;
         message.BodyEncoding = System.Text.Encoding.UTF8;
         message.IsBodyHtml = true;
         SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
         NetworkCredential basicCredential1 = new NetworkCredential("tutuatlantica@gmail.com", "Kakikubau");
         client.EnableSsl = true;
         client.UseDefaultCredentials = false;
         client.Credentials = basicCredential1;

         try
         {
            client.Send(message);

            // update account table
            var account = context.Accounts.Find(employee.NIK);

            account.OTP = otp;
            account.ExpiredToken = DateTime.Now.AddMinutes(5);
            account.isUsed = false;
            context.SaveChanges();

            result.Status = 200;
            return result;
         }

         catch (Exception ex)
         {
            throw ex;
         }
      }


      public Result ChangePassword(ChangePasswordVM changePasswordVM)
      {
         var employeeAcount = (
            from employee in context.Employees
            join account in context.Accounts
               on employee.NIK equals account.NIK
            where employee.Email == changePasswordVM.Email
            select new
            {
               NIK = employee.NIK,
               employee.Email,
               account.Password,
               account.OTP,
               account.ExpiredToken,
               account.isUsed

            }
         ).FirstOrDefault();

         // jika null return akun tidak dapat ditemukan!
         if (isError(employeeAcount == null, 1)) return result;

         // Jika otp tidak sama return OTP salah, error
         if (isError(employeeAcount.OTP != changePasswordVM.OTP, 2)) return result;

         // Jika otp sudah digunakan return error
         if (isError(employeeAcount.isUsed, 3)) return result;

         // Jika otp sudah expired, return error
         if (isError(employeeAcount.ExpiredToken < DateTime.Now, 4)) return result;

         // Jika newPassword tidak sama dengan confirmPassword, return error
         if (isError(changePasswordVM.NewPassword != changePasswordVM.Confirmpassword, 5)) return result;

         // update data account
         var accountUpdate = context.Accounts.Find(employeeAcount.NIK);
         accountUpdate.Password = BC.HashPassword(changePasswordVM.Confirmpassword);
         accountUpdate.isUsed = true;
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
      private string generatedNIK()
      {
         var yearNow = DateTime.Now.ToString("yyyy");

         // var maxNIK = context.Employees.LastOrDefault().NIK; // Get NIK tertinggi
         var niks = context.Employees.AsNoTracking().ToList(); // Get NIKS
         if (niks.Count == 0) return yearNow + "001";

         var nikHight = niks.Max(e => Int32.Parse(e.NIK)).ToString(); // cari nik tertinggi

         // Validasi jika tahun berganti maka dibuat menjadi 001 lagi.
         var yearEmployeeCreated = nikHight.Substring(0, 4);

         if (yearEmployeeCreated == yearNow)
         {
            var identityNIK = Int32.Parse(nikHight.Substring(6)) + 1;
            return yearNow + "00" + identityNIK.ToString();
         }
         else
         {
            return yearNow + "001";
         }
      }

      private int ValidationPhoneOrEmailIsExist(string NIK, string Phone, string Email)
      {
         var checkPhone = context.Employees.Any(e => e.Phone == Phone && e.NIK != NIK);
         if (checkPhone) return 1;

         var checkEmail = context.Employees.Any(e => e.Email == Email && e.NIK != NIK);
         if (checkEmail) return 2;

         return 0;
      }
   }
}
