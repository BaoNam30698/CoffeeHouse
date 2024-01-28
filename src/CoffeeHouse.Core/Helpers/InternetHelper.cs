using CoffeeHouse.Core.Misc;

namespace CoffeeHouse.Core.Helpers
{
    public class InternetHelper
    {
        private static bool ConnectedToInternet = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

        public InternetHelper()
        {
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public static InternetHelper Create()
        {
            return new InternetHelper();
        }

        public static bool HasInternet()
        {
            return ConnectedToInternet;
        }

        public static async Task DisplayNoConnection()
        {
            await Shell.Current.DisplayAlert("", "No internet connection", "OK");
        }

        private static void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectedToInternet = e.NetworkAccess.HasFlag(NetworkAccess.Internet);
            if (ConnectedToInternet)
            {
                HideNoInternetOverlay();
            }
            else
            {
                ShowNoInternetOverlay();
            }
        }

        public static void ShowNoInternetOverlay()
        {
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                var window = Shell.Current.CurrentPage.GetParentWindow();
                if (window is not null)
                {
                    ((OverlayWindow)window).ShowNoInternetOverlay();
                }
            }
            else
            {
                MessagingCenter.Send("this", MessagingCenterConstants.ShowOrHideNoInternet, true);
            }
        }

        public static void HideNoInternetOverlay()
        {
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                var window = Shell.Current.CurrentPage.GetParentWindow();
                if (window is not null)
                {
                    ((OverlayWindow)window).HideNoInternetOverlay();
                }
            }
            else
            {
                MessagingCenter.Send("this", MessagingCenterConstants.ShowOrHideNoInternet, false);
            }
        }
    }

    public class OverlayWindow : Window, IVisualTreeElement
    {
        private readonly NoInternetOverlay _noInternetOverlay;

        public OverlayWindow(Page page) : base(page)
        {
            _noInternetOverlay = new NoInternetOverlay(this) { IsVisible = false };
        }

        public void ShowNoInternetOverlay() => _noInternetOverlay.IsVisible = true;

        public void HideNoInternetOverlay() => _noInternetOverlay.IsVisible = false;

        protected override void OnCreated()
        {
            AddOverlay(this._noInternetOverlay);
            base.OnCreated();
        }

        public IVisualTreeElement GetVisualParent() => Application.Current;
    }

    public class NoInternetOverlay : WindowOverlay
    {
        public NoInternetOverlay(IWindow window)
            : base(window)
        {
            AddWindowElement(new NoInternetElementOverlay(this));
        }

        class NoInternetElementOverlay : IWindowOverlayElement
        {
            RectF _box = new RectF(0, 0, 5000, 80);
            readonly WindowOverlay _overlay;

            public NoInternetElementOverlay(WindowOverlay overlay)
            {
                _overlay = overlay;
            }

            public string CurrentText { get; set; } = "No internet connection \n ";

            public bool Contains(Point point) =>
                _box.Contains(new Point(point.X / _overlay.Density, point.Y / _overlay.Density));

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.FillColor = Color.FromArgb("#132638");
                canvas.StrokeColor = Colors.White;
                canvas.FontColor = Colors.White;
                canvas.FontSize = 14f;
                canvas.FillRectangle(_box);
                canvas.DrawString(CurrentText, 0, 0, dirtyRect.Width, 80, HorizontalAlignment.Center, VerticalAlignment.Bottom);
            }
        }
    }
}
