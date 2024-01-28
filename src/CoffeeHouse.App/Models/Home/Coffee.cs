using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHouse.App.Models.Home
{
    public class CoffeeResponse
    {
        public IEnumerable<CoffeeDto> Coffee { get; set; }
        public IEnumerable<CoffeeDto> CoffeeBeans { get; set; }
    }

    public class Coffee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }

    public class CoffeeDto : Coffee
    {

    }
}
