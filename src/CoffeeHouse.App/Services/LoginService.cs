using CoffeeHouse.App.Models.Login;
using CoffeeHouse.App.Services.Interfaces;
using CoffeeHouse.Core;
using CoffeeHouse.Core.Extensions;
using CoffeeHouse.Core.Misc;
using System.Net;

namespace CoffeeHouse.App.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAppDelegatingHandler _appDelegatingHandler;

        public LoginService(IAppDelegatingHandler appDelegatingHandler)
        {
            _appDelegatingHandler = appDelegatingHandler;
        }

        public async Task<GeneralResponse<UserAuthentication>> Authenticate(string userName, string password)
        {
            var httpClient = await _appDelegatingHandler.CreateHttpClient();

            //var response = await httpClient.Post<UserAuthentication>("/v1/authenticate", new { userName, password });

            var response = new GeneralResponse<UserAuthentication>()
            {
                IsSuccess = true,
                Content = new UserAuthentication()
                {
                    CanLogin = true,
                }
            };

            return response;
        }
    }
}
