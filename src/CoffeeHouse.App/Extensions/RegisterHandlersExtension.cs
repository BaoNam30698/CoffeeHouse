#if IOS
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
#endif

namespace CoffeeHouse.App.Extensions
{
    public static partial class ConfigExtensions
    {
        public static MauiAppBuilder RegisterHandlers(this MauiAppBuilder builder)
        {
            RegisterMappers();

            return builder.ConfigureMauiHandlers(handlers =>
            {
                // Your handlers here...
                handlers.AddHandler(typeof(CarouselView), typeof(CustomCarouseHandler));
#if IOS
                handlers.AddHandler(typeof(Shell), typeof(CustomShellRenderer));
#endif
            });
        }

        private static void RegisterMappers()
        {
            // For a large mapper its better to create a separete file for it.
           
        }
    }

    /// <summary>
    /// hide default Indicator when swping carousel on iOS
    /// </summary>
    public partial class CustomCarouseHandler : Microsoft.Maui.Controls.Handlers.Items.CarouselViewHandler
    {
#if IOS
        protected override void ScrollToRequested(object sender, ScrollToRequestEventArgs args)
        {

            Controller.CollectionView.ShowsHorizontalScrollIndicator = false;
            Controller.CollectionView.ShowsVerticalScrollIndicator = false;

            base.ScrollToRequested(sender, args);
        }
#endif
    }

#if IOS
    // disable swipe back gesture on iOS
    public class CustomShellRenderer : ShellRenderer
    {
        public CustomShellRenderer()
        {

        }
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            return new CustomSectionRenderer(this);
        }
    }

    public class CustomSectionRenderer : ShellSectionRenderer
    {
        public CustomSectionRenderer(IShellContext context) : base(context)
        {

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InteractivePopGestureRecognizer.Enabled = false;
        }
    }
#endif
}
