using api.Models;
using api.ViewModel;
using client.Base;
using client.Models;
using client.Repositories;
using client.ViewModel;
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
   public class LoginRepository : GeneralRepository<LoginVM, string>
   {
      private readonly Address address;
      private readonly HttpClient httpClient;
      private readonly string request;
      private readonly IHttpContextAccessor _contextAccessor;

      public LoginRepository(Address address, string request = "accounts/") : base(address, request)
      {
         this.address = address;
         this.request = request;
         _contextAccessor = new HttpContextAccessor();
         httpClient = new HttpClient
         {
            BaseAddress = new Uri(address.link)
         };
      }

      public async Task<JwtResponseVM> Auth(LoginVM loginVM)
      {
         JwtResponseVM jwt = null;

         StringContent content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
         var result = await httpClient.PostAsync(address.link + request + "login", content);

         string apiResponse = await result.Content.ReadAsStringAsync();

         jwt = JsonConvert.DeserializeObject<JwtResponseVM>(apiResponse);
         return jwt;
      }

   }
}