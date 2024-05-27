using BlazorBigexecutionExample.Models;

namespace BlazorBigexecutionExample.Services
{
    public class UniversityService : BaseService, IUniversityService
    {
        private string baseUrl = $"http://universities.hipolabs.com";
        public UniversityService(HttpClient httpClient) : base(httpClient) { }

        public async Task<IEnumerable<University>> GetUniversities()
        {
            return await GetMethodList<University>($"search?country=Korea,+Republic+of");
        }
    }
}
