using CoffeeHouse.Core.Injectable;
using CoffeeHouse.Core.Misc;
using CommunityToolkit.Mvvm.Input;

namespace CoffeeHouse.App.Modules.Shared.Views
{
    public partial class NavigationViewModel : ViewModelBase, ITransientDependency
    {
        [RelayCommand]
        public static async Task GoTo(TabType tabType)
        {
            switch (tabType)
            {
                case TabType.Home: await GoToAsync($"//{PageConstants.HomePage}"); break;
                case TabType.Card: await GoToAsync(PageConstants.HomePage); break;
                case TabType.Favorite: await GoToAsync(PageConstants.HomePage); break;
                case TabType.Notification: await GoToAsync(PageConstants.HomePage); break;
            }
        }
    }
}
