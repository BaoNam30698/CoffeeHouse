using CoffeeHouse.App.Modules.Shared.Modals;
using CoffeeHouse.App.Services.Interfaces;
using CoffeeHouse.Core.Injectable;
using CoffeeHouse.Core.Misc;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;

namespace CoffeeHouse.App.Modules.LoginComponent
{
    public partial class LoginViewModel : ViewModelBase, ITransientDependency
    {
        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string password;

        private readonly ILoginService _loginService;
        private readonly IPopupNavigation _popupNavigation;

        public LoginViewModel(ILoginService loginService, IPopupNavigation popupNavigation)
        {
            _loginService = loginService;
            _popupNavigation = popupNavigation;
        }

        public async Task Initialize()
        {
            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task Authenticate()
        {
            var response = await _loginService.Authenticate(UserName, Password);

            if (response.IsSuccess && (response.Content?.CanLogin ?? false))
            {
                await GoToAsync($"//{PageConstants.HomePage}");
            }
            else
            {
                await _popupNavigation.PushAsync(new MessageDialogPage("", 0, 0, "", response.ErrorMessage ?? "An error occurred please try again", "OK"));
            }
        }
    }
}
