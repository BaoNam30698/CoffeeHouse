using CoffeeHouse.App.Models.Home;
using CoffeeHouse.App.Services;
using CoffeeHouse.Core.Injectable;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CoffeeHouse.App.Modules.HomeComponent
{
    public partial class HomeViewModel : ViewModelBase, ITransientDependency
    {
        [ObservableProperty]
        private string textSearch;

        [ObservableProperty]
        private ObservableCollection<CoffeeDto> coffeeCollection;

        [ObservableProperty]
        private ObservableCollection<CoffeeDto> coffeeBeansCollection;

        private readonly CoffeeService _coffeeService;

        public HomeViewModel(CoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        public async Task Initialize()
        {
            var result = await _coffeeService.GetCoffee();

            CoffeeCollection = new()
            {
                new CoffeeDto
                {
                    Name = "Americano"
                },
                new CoffeeDto
                {
                    Name = "Cappuchino"
                },
                new CoffeeDto
                {
                    Name = "Black Coffee"
                },
                new CoffeeDto
                {
                    Name = "Espresso"
                },
                new CoffeeDto
                {
                    Name = "Latte"
                },
                new CoffeeDto
                {
                    Name = "Macchiato"
                },
            };

            CoffeeBeansCollection = new()
            {
                new CoffeeDto
                {
                    Name = "Arabica Beans"
                },
                new CoffeeDto
                {
                    Name = "Excelsa Beans"
                },
                new CoffeeDto
                {
                    Name = "Liberica Beans"
                },
                new CoffeeDto
                {
                    Name = "Robusta Beans"
                },
            };
        }
    }
}
