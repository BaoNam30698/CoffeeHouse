using CoffeeHouse.App.Models.Login;
using CoffeeHouse.Core.Injectable;
using CoffeeHouse.Core.Misc;

namespace CoffeeHouse.App.Services.Interfaces
{
    public interface ILoginService : ITransientDependency
    {
        Task<GeneralResponse<UserAuthentication>> Authenticate(string userName, string password);
    }
}
