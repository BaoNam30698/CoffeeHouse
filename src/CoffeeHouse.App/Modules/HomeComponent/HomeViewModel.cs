using CoffeeHouse.App.Models.Home;
using CoffeeHouse.App.Services;
using CoffeeHouse.Core.Extensions;
using CoffeeHouse.Core.Injectable;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        [ObservableProperty]
        private ObservableCollection<TypeOfCoffee> typeOfCoffee;

        private readonly CoffeeService _coffeeService;

        public HomeViewModel(CoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        public async Task Initialize()
        {
            var result = await _coffeeService.GetCoffee();

            TypeOfCoffee = new ObservableCollection<TypeOfCoffee>()
            {
                new TypeOfCoffee()
                {
                    Name = "All",
                    TextColor = Color.FromArgb("#D17842"),
                    DotOpacity = 1
                },
                new TypeOfCoffee()
                {
                    Name = "Americano"
                },
                new TypeOfCoffee()
                {
                    Name = "Cappuchino"
                },
                new TypeOfCoffee()
                {
                    Name = "Black Coffee"
                },
                new TypeOfCoffee()
                {
                    Name = "Espresso"
                },
                new TypeOfCoffee()
                {
                    Name = "Latte"
                },
                new TypeOfCoffee()
                {
                    Name = "Macchiato"
                },
            };

            CoffeeCollection = new()
            {
                new CoffeeDto
                {
                    Id = "C1",
                    Name = "Americano",
                    ImageUrl = "americano_pic_1_square.png",
                    ShortDescription = "with Foam",
                    Price = "4.20"
                },
                new CoffeeDto
                {
                    Id = "C2",
                    Name = "Cappuchino",
                    ImageUrl = "cappuccino_pic_2_square.png",
                    ShortDescription = "With Steamed Milk",
                    Price = "4.20"
                },
                new CoffeeDto
                {   
                    Id = "C3",
                    Name = "Cappuchino",
                    ImageUrl = "cappuccino_pic_2_square.png",
                    ShortDescription = "With Steamed Milk",
                    Price = "4.20"
                },
                new CoffeeDto
                {   Id = "C4",
                    Name = "Black Coffee",
                    ImageUrl = "black_coffee_pic_1_square.png",
                    ShortDescription = "With Steamed Milk",
                    Price = "4.20"
                },
                new CoffeeDto
                {
                    Id = "C5",
                    Name = "Espresso",
                    ImageUrl = "cappuccino_pic_2_square.png",
                    ShortDescription = "With Steamed Milk",
                    Price = "4.20"
                },
                new CoffeeDto
                {
                    Id = "C6",
                    Name = "Latte",
                    ImageUrl = "cappuccino_pic_2_square.png",
                    ShortDescription = "With Steamed Milk",
                    Price = "4.20"
                },
                new CoffeeDto
                {
                    Id = "C7",
                    Name = "Macchiato",
                    ImageUrl = "cappuccino_pic_2_square.png",
                    ShortDescription = "With Steamed Milk",
                    Price = "4.20"
                },
            };

            CoffeeBeansCollection = new()
            {
                new CoffeeDto
                {
                    Id = "B1",
                    Name = "Arabica Beans",
                    ImageUrl = "arabica_coffee_beans_square.png",
                    ShortDescription = "From Africa",
                    Price = "4.20"
                },
                new CoffeeDto
                {
                    Id = "B2",
                    Name = "Excelsa Beans",
                    ImageUrl = "excelsa_coffee_beans_square.png",
                    ShortDescription = "From Malaysia",
                    Price = "4.20"
                },
                new CoffeeDto
                {
                    Id = "B3",
                    Name = "Liberica Beans",
                    ImageUrl = "liberica_coffee_beans_square.png",
                    ShortDescription = "From Malaysia",
                    Price = "4.20"
                },
                new CoffeeDto
                {
                    Id = "B4",
                    Name = "Robusta Beans",
                    ImageUrl = "robusta_coffee_beans_square.png",
                    ShortDescription = "From VietNam",
                    Price = "4.20"
                },
            };
        }

        [RelayCommand]
        private async Task TypeOfCoffeeTapped(TypeOfCoffee item)
        {
            TypeOfCoffee.ForEach(x => { x.DotOpacity = 0; x.TextColor = Color.FromArgb("#AEAEAE"); });

            item.TextColor = Color.FromArgb("#D17842");
            item.DotOpacity = 1;

            await Task.CompletedTask;
        }
    }
}
