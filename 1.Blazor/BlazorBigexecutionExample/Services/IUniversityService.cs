using BlazorBigexecutionExample.Models;

namespace BlazorBigexecutionExample.Services
{
    public interface IUniversityService
    {
        Task<IEnumerable<University>> GetUniversities();
    }
}
