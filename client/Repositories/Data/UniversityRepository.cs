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
   public class UniversityRepository : GeneralRepository<University, int>
   {
      private readonly Address address;
      private readonly HttpClient httpClient;
      private readonly string request;
      private readonly IHttpContextAccessor _contextAccessor;

      public UniversityRepository(Address address, string request = "universities/") : base(address, request)
      {
         this.address = address;
         this.request = request;
         _contextAccessor = new HttpContextAccessor();
         httpClient = new HttpClient
         {
            BaseAddress = new Uri(address.link)
         };
      }

      public async Task<object> GetUniversities()
      {
         /// isi codingan kalian disini
         object result;

         using (var response = await httpClient.GetAsync(address.link + request))
         {
            string apiResponse = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject(apiResponse);
         }

         return result;
      }
   }
}