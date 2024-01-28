using CoffeeHouse.App.Models.Home;
using CoffeeHouse.Core.Injectable;

namespace CoffeeHouse.App.Services.Interfaces
{
    public interface ICoffeeService : ITransientDependency
    {
        Task<CoffeeResponse> GetCoffee();
    }
}
