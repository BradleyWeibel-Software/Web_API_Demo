using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web_API_Demo.Authority;

namespace WebApp.Data
{
    public class WebAPIExecuter : IWebApiExecuter
    {
        private const string apiName = "ShirtsApi";
        private const string authApiName = "AuthorityApi";
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public WebAPIExecuter(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.httpContextAccessor = httpContext;
        }

        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJWTToHeader(httpClient);
            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
            var response = await httpClient.SendAsync(request);

            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJWTToHeader(httpClient);
            var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);

            await HandlePotentialError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string relativeUrl, T obj)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJWTToHeader(httpClient);
            var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);

            await HandlePotentialError(response);
        }

        public async Task InvokeDelete(string relativeUrl)
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            await AddJWTToHeader(httpClient);
            var response = await httpClient.DeleteAsync(relativeUrl);

            await HandlePotentialError(response);
        }

        private async Task HandlePotentialError(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorJson = await httpResponse.Content.ReadAsStringAsync();
                throw new WebApiException(errorJson);
            }
        }

        private async Task AddJWTToHeader(HttpClient httpClient)
        {
            JWTToken? token = null;
            string? strToken = httpContextAccessor.HttpContext?.Session.GetString("access_token");
            if (!string.IsNullOrWhiteSpace(strToken))
            {
                // Token was already created, reuse it
                token = JsonConvert.DeserializeObject<JWTToken>(strToken);
            }

            if (token == null || token.ExpiresAt <= DateTime.UtcNow)
            {
                // No token was yet created or has expired - Create a new one
                var clientId = configuration.GetValue<string>("ClientId");
                var secret = configuration.GetValue<string>("Secret");

                // Authenticate
                var authoClient = httpClientFactory.CreateClient(authApiName);
                var response = await authoClient.PostAsJsonAsync("auth", new AppCredential
                {
                    ClientId = clientId,
                    Secret = secret
                });
                response.EnsureSuccessStatusCode();

                // Get the JWT
                strToken = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<JWTToken>(strToken);

                httpContextAccessor.HttpContext?.Session.SetString("access_token", strToken);
            }

            // Pass the JWT to endpoints through the http headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token?.AccessToken);
        }
    }
}
