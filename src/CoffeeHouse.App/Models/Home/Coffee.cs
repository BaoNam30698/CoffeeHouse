using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeHouse.App.Models.Home
{
    public class CoffeeResponse
    {
        public IEnumerable<string> TypeOfCoffee { get; set; }
        public IEnumerable<CoffeeDto> Coffee { get; set; }
        public IEnumerable<CoffeeDto> CoffeeBeans { get; set; }
    }

    public class Coffee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }

    public class CoffeeDto : Coffee
    {

    }

    public class TypeOfCoffee : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private Color _textColor = Color.FromArgb("#AEAEAE");
        public Color TextColor
        {
            get => _textColor;
            set => SetProperty(ref _textColor, value);
        }

        private double _dotOpacity = 0d;
        public double DotOpacity
        {
            get => _dotOpacity;
            set => SetProperty(ref _dotOpacity, value);
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
     [CallerMemberName] string propertyName = "",
     Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
