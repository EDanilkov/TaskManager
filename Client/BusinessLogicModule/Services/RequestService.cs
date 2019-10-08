using BusinessLogicModule.Properties;
using Newtonsoft.Json;
using SharedServicesModule.ResponseModel;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicModule.Services
{
    public static class RequestService
    {
        static HttpClient _client = new HttpClient();
        
        public static async Task<T> Get<T>(string path)
        {
            _client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Resources.Token);
            HttpResponseMessage response = await _client.GetAsync(new Uri(path));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<NewResponseModel> Post(string path, string json)
        {
            _client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Resources.Token);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(path, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<NewResponseModel> Put(string path, string json)
        {
            _client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Resources.Token);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(path, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<NewResponseModel> Delete(string path)
        {
            _client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", Resources.Token);
            HttpResponseMessage response = await _client.DeleteAsync(new Uri(path));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error status code: " + (int)response.StatusCode + " - " + response.StatusCode.ToString());
            }
            return JsonConvert.DeserializeObject<NewResponseModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
