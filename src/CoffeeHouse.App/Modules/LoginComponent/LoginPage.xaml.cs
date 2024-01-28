using CoffeeHouse.Core.Injectable;

namespace CoffeeHouse.App.Modules.LoginComponent;

public partial class LoginPage : ContentPage, ITransientDependency
{
    private bool IsAlreadyLoaded = false;

    private readonly LoginViewModel _vm;

    public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}

    protected override async void OnAppearing()
    {
        if (!IsAlreadyLoaded)
        {
            IsAlreadyLoaded = true;
            await _vm.Initialize();
        }

        base.OnAppearing();
    }
}