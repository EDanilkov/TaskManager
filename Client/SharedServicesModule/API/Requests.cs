using Newtonsoft.Json;
using SharedServicesModule;
using SharedServicesModule.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicModule.API
{
    public static class Requests //HttpHelper (Manager) RequestHelper
    {
        static HttpClient client = new HttpClient();

        public static async Task<T> Get<T>(string path)//, string token)
        {
            
            client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Resources.Token);
            HttpResponseMessage response = await client.GetAsync(new Uri(path));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        }

        public static async Task<NewResponseModel> Post(string path, string json)
        {
            client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Resources.Token);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(path, content);
            var a = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<NewResponseModel> Put(string path, string json)
        {
            client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Resources.Token);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(path, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<NewResponseModel> Delete(string path)
        {
            client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Resources.Token);
            HttpResponseMessage response = await client.DeleteAsync(new Uri(path));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
