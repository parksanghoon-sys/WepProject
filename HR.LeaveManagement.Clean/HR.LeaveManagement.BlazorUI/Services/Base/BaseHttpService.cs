using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        public IClient _client;
        protected readonly ILocalStorageService _localStorage;
        public BaseHttpService(IClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorage = localStorageService;
        }
        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Invalid data was submitted", ValidationErrors = ex.Response, IsSuccess = false };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "The record was not found.", IsSuccess = false };
            }
            else
            {
                return new Response<Guid>() { Message = "Something went wrong, please try again later.", IsSuccess = false };
            }
        }
        protected async Task AddBearerToken()
        {
            if (await _localStorage.ContainKeyAsync("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));
        }
    }
}
