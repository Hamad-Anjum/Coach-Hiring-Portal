using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using CHS.Infrastructure.ViewModels;
using CHS.Web.Authentication;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using Newtonsoft.Json;

namespace CHS.Web.Services
{
    public class Service : IService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public Service(ProtectedLocalStorage localStorage, HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<HttpResponseMessage> Send(string url)
        {
            var token = await _localStorage.GetAsync<string>("token");
            if (!string.IsNullOrEmpty(token.Value))
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{ Constant.ApiUrl}{url}");
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

                var response = await _httpClient.SendAsync(httpRequestMessage);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return response;
                }
            }
            return null;
        }

        public async Task<HttpResponseMessage> SendRequest(string httpMethod, string url, string serializedData = null)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(
                httpMethod.ToLower() == "get" ? HttpMethod.Get :
                httpMethod.ToLower() == "post" ? HttpMethod.Post :
                httpMethod.ToLower() == "put" ? HttpMethod.Put :
                httpMethod.ToLower() == "delete" ? HttpMethod.Delete :
                HttpMethod.Patch, $"{Constant.ApiUrl}{url}");

            var token = await _localStorage.GetAsync<string>("token");
            //if (!string.IsNullOrEmpty(token.Value))
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value ?? "");

            if (httpMethod.ToLower() != "get")
            {
                if (!string.IsNullOrEmpty(serializedData))
                {
                    httpRequestMessage.Content = new StringContent(serializedData);
                    httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
            }

            var response = await _httpClient.SendAsync(httpRequestMessage);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _ = ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsNotAuthenticated();
            }
            return response;
        }

        public async Task<HttpStatusCode> Post(HttpResponseMessage response)
        {
            if (response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<HttpStatusCode>(responseBody);
                return responseResult;
            }
            return HttpStatusCode.BadRequest;
        }

        public async Task<HttpStatusCode> Put(HttpResponseMessage response)
        {
            if (response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<HttpStatusCode>(responseBody);
                return responseResult;
            }
            return HttpStatusCode.BadRequest;
        }

        public async Task<HttpStatusCode> Delete(HttpResponseMessage response)
        {
            if (response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<HttpStatusCode>(responseBody);
                return responseResult;
            }
            return HttpStatusCode.BadRequest;
        }

        public async Task<T> Get<T>(HttpResponseMessage response)
        {
            if (response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<T>(responseBody);
                return responseResult ?? default;
            }
            return default;
        }

        public async Task<ICollection<T>> GetAll<T>(HttpResponseMessage response) where T : class
        {
            if (response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var responseResult = JsonConvert.DeserializeObject<ICollection<T>>(responseBody);

                return await Task.FromResult(responseResult ?? null);
            }
            return null;
        }
    }

}
