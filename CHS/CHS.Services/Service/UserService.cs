using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Services.IService;

using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using Newtonsoft.Json;

namespace CHS.Services.Service
{
    public class UserService : IUserService
    {
        private readonly ProtectedLocalStorage _localStorage;

        public HttpClient HttpClient { get; }

        public UserService(HttpClient httpClient, ProtectedLocalStorage localStorage)
        {
            HttpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<LoginViewModel> LoginAsync(LoginViewModel model)
        {
            string serializedUser = JsonConvert.SerializeObject(model);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "Account/Login")
            {
                Content = new StringContent(serializedUser)
            };
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.SendAsync(httpRequestMessage);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var returnedStatusCode = JsonConvert.DeserializeObject<LoginViewModel>(responseBody);
                return await Task.FromResult(returnedStatusCode ?? new LoginViewModel());
            }
            return await Task.FromResult(new LoginViewModel());
        }

        public async Task<RegisterViewModel> RegisterAsync(RegisterViewModel model)
        {
            string serializedUser = JsonConvert.SerializeObject(model);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "Account/Register")
            {
                Content = new StringContent(serializedUser)
            };

            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var returnedStatusCode = JsonConvert.DeserializeObject<RegisterViewModel>(responseBody);
                return await Task.FromResult(returnedStatusCode ?? new RegisterViewModel());
            }
            return await Task.FromResult(new RegisterViewModel());
        }

        public async Task<UserWithToken> RefreshTokenAsync(RefreshViewModel model)
        {
            string serializedUser = JsonConvert.SerializeObject(model);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "Account/RefreshToken")
            {
                Content = new StringContent(serializedUser)
            };
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.SendAsync(httpRequestMessage);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var returnedStatusCode = JsonConvert.DeserializeObject<UserWithToken>(responseBody);
                return await Task.FromResult(returnedStatusCode ?? new UserWithToken());
            }
            return await Task.FromResult(new UserWithToken());
        }
    }

}
