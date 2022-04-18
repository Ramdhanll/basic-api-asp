using api.Models;
using api.ViewModel;
using client.Base;
using client.ViewModel;
using client.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace client.Repositories.Data
{
   public class EmployeeRepository : GeneralRepository<Employee, string>
   {
      // private readonly Address address;
      // private readonly HttpClient httpClient;
      // private readonly string request;
      // private readonly IHttpContextAccessor _contextAccessor;

      public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
      {
         // this.address = address;
         // this.request = request;
         // _contextAccessor = new HttpContextAccessor();
         // httpClient = new HttpClient
         // {
         //    BaseAddress = new Uri(address.link)
         // };
         //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
      }
   }
}