using CoffeeHouse.Core.Injectable;

namespace CoffeeHouse.App.Modules.HomeComponent;

public partial class HomePage : ContentPage, ITransientDependency
{
    private bool IsAlreadyLoaded = false;

    private readonly HomeViewModel _vm;

    public HomePage(HomeViewModel vm)
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

    private void OnInputSearchFocused(object sender, FocusEventArgs e)
    {
        ImgSearch.IsVisible = false;
    }

    private void OnInputSearchUnFocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(_vm.TextSearch))
        {
            ImgSearch.IsVisible = true;
        }
    }
}