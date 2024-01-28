using CoffeeHouse.Core.Misc;
using System.Diagnostics;

namespace CoffeeHouse.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            var task = InitAsync();

            task.ContinueWith((task) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var result = await task;

                    await Shell.Current.GoToAsync($"//{PageConstants.LoginPage}");
                });
            });

            base.OnStart();
        }

        private static async Task<string> InitAsync()
        {
            await Task.Delay(1000);
            return "Hola";
        }
    }
}