using api.Models;
using api.ViewModel;
using client.Base;
using client.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace client.Repositories.Data
{
   public class AccountRepository : GeneralRepository<Account, string>
   {
      private readonly Address address;
      private readonly HttpClient httpClient;
      private readonly string request;
      private readonly IHttpContextAccessor _contextAccessor;

      public AccountRepository(Address address, string request = "accounts/") : base(address, request)
      {
         this.address = address;
         this.request = request;
         _contextAccessor = new HttpContextAccessor();
         httpClient = new HttpClient
         {
            BaseAddress = new Uri(address.link)
         };
         httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
      }

      public async Task<object> GetMaster()
      {
         /// isi codingan kalian disini
         object result;

         using (var response = await httpClient.GetAsync(address.link + request + "master"))
         {
            string apiResponse = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject(apiResponse);
         }

         // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));


         return result;
      }

      public async Task<object> Register(RegisterVM registerVM)
      {
         /// isi codingan kalian disini
         object result;

         StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");

         using (var response = httpClient.PostAsync(address.link + request + "register/", content).Result)
         {
            string apiResponse = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject(apiResponse);
         }

         return result;
      }

      // public async Task<object> GetMasterByNIK(string nik)
      // {
      //    /// isi codingan kalian disini
      //    object result;

      //    using (var response = await httpClient.GetAsync(address.link + request + "master/" + nik))
      //    {
      //       string apiResponse = await response.Content.ReadAsStringAsync();
      //       Console.Write(apiResponse);
      //       result = JsonConvert.DeserializeObject(apiResponse);
      //    }

      //    return result;
      // }

   }
}