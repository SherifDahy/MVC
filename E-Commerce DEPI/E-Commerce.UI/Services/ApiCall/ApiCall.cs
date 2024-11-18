using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ApiConsume
{
    public class ApiCall : IApiCall
    {
        string baseUrl = null;
        HttpClient client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiCall(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            baseUrl = configuration.GetSection("api")["baseUrl"];
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);


        }
       


        public async Task<IEnumerable<T>> GetAllAsync<T>(string apiName)
        {
            SetToken();
            var response = await client.GetAsync(apiName);
            response.EnsureSuccessStatusCode();
            var temp = await response.Content.ReadAsAsync<IEnumerable<T>>();
            return temp;
        }
        public async Task<T> GetByIdAsync<T>(string url, int id)
        {
            SetToken();
            var response = await client.GetAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<bool> CreateAsync<T>(string url, T entity)
        {
            SetToken();
            var response = await client.PostAsJsonAsync(url, entity);
            return response.IsSuccessStatusCode;
        }
        public async Task<T2> PostReturnAsync<T1, T2>(string url, T1 entity)
        {
            SetToken();
            var response = await client.PostAsJsonAsync(url, entity);
            string responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T2>(responseContent);
            return result;
        }
        public async Task<bool> UpdateAsync<T>(string url, int id, T entity)
        {
            SetToken();
            var response = await client.PutAsJsonAsync($"{url}/{id}", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync<T>(string url, int id)
        {
            SetToken();
            var response = await client.DeleteAsync($"{url}?id={id}");
            return response.IsSuccessStatusCode;
        }

        private void SetToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var _token = httpContext.Request.Cookies["token"];

            if (_token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

       
        public async Task<IEnumerable<T>> GetAllByIdAsync<T>(string url, int id)
        {
            SetToken();
            var response = await client.GetAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IEnumerable<T>>();
        }
    }
}
