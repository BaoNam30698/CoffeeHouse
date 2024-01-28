using CoffeeHouse.Core.Helpers;
using Mopups.Interfaces;

namespace CoffeeHouse.App.Modules.Shared.Modals;

public partial class MessageDialogPage
{
    public event EventHandler Ok;

    public MessageDialogPage(string imgSource, int imgHeight, int imgWidth, string title, string message, string buttonText = "Cancel")
    {
        InitializeComponent();

        if (string.IsNullOrEmpty(imgSource))
        {
            ImgIcon.IsVisible = false;
        }
        else
        {
            ImgIcon.Source = imgSource;
            ImgIcon.HeightRequest = imgHeight;
            ImgIcon.WidthRequest = imgWidth;
        }

        if (string.IsNullOrEmpty(title))
        {
            LblTitle.IsVisible = false;
        }
        else
        {
            LblTitle.Text = title;
        }

        if (string.IsNullOrEmpty(message))
        {
            LblMessage.IsVisible = false;
        }
        else
        {
            if (string.IsNullOrEmpty(imgSource) && string.IsNullOrEmpty(title))
            {
                LblMessage.Margin = new Thickness(0, 15, 0, 0);
            }

            LblMessage.Text = message;
        }

        BtnCancel.Text = buttonText;
    }

    private async void OnBtnCancelClicked(object sender, EventArgs e)
    {
        await ServiceHelper.GetService<IPopupNavigation>().PopAsync();
        Ok?.Invoke(this, e);
    }
}