using CoffeeHouse.App.Models.Home;
using CoffeeHouse.App.Services.Interfaces;
using CoffeeHouse.Core;

namespace CoffeeHouse.App.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly IAppDelegatingHandler _appDelegatingHandler;

        public CoffeeService(IAppDelegatingHandler appDelegatingHandler)
        {
            _appDelegatingHandler = appDelegatingHandler;
        }

        public async Task<CoffeeResponse> GetCoffee()
        {
            var httpClient = await _appDelegatingHandler.CreateHttpClient();

            //var response = await httpClient.Post<UserAuthentication>("/v1/authenticate", new { userName, password });

            var response = new CoffeeResponse();

            return response;
        }
    }
}
