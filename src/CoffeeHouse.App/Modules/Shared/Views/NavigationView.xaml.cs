using CoffeeHouse.Core.Helpers;
using CoffeeHouse.Core.Injectable;

namespace CoffeeHouse.App.Modules.Shared.Views;

public partial class NavigationView : ContentView, ITransientDependency
{
	public NavigationView()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<NavigationViewModel>();
	}
}