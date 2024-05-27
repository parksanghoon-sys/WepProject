using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorBigexecutionExample.Services
{
    public class BaseService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }

        public async Task<IEnumerable<TResult>> GetMethodList<TResult>(string url)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync
                            <IEnumerable<TResult>>(responseStream);
                }

                return null;

            }
            catch (HttpRequestException e)
            {                
                return null;
            }
        }

        public async Task<TResult> GetMethod<TResult>(string url)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync
                            <TResult>(responseStream);
                }

                return default(TResult);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(TResult);
            }
        }

        public async Task PostMethodList<T>(string url, List<T> content)
        {
            await _httpClient.PostAsJsonAsync(url, content);
        }

        public async Task PostMethod<T>(string url, T content)
        {
            await _httpClient.PostAsJsonAsync(url, content);
        }

        public async Task<TResult> PostMethodGetObject<TResult, TValue>(string url, TValue content)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, content);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync
                            <TResult>(responseStream);
                }
                return default(TResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(TResult);
            }
        }
    }
}
