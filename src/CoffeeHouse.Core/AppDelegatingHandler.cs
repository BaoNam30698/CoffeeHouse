using CoffeeHouse.Core.Injectable;
using Microsoft.Extensions.Configuration;

namespace CoffeeHouse.Core
{
    public interface IAppDelegatingHandler : ITransientDependency
    {
        Task<HttpClient> CreateHttpClient(bool allowAnonymous = false);
    }

    public class AppDelegatingHandler : IAppDelegatingHandler
    {
        private readonly HttpClient _httpClient;

        public AppDelegatingHandler(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Mintec-RequestType", "Maui");
            _httpClient.BaseAddress = new Uri(configuration.GetRequiredSection(nameof(AppSettings.EnvironmentSettings)).Get<AppSettings.EnvironmentSettings>().Authority);
        }

        public async Task<HttpClient> CreateHttpClient(bool allowAnonymous = false)
        {
            return await Task.FromResult(_httpClient);
        }
    }
}
