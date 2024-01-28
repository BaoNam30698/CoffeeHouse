using CommunityToolkit.Mvvm.ComponentModel;

namespace CoffeeHouse.App
{
    public class ViewModelBase : ObservableObject
    {
        public static void MakeACallBase(string phoneNumber)
        {
            if (PhoneDialer.Default.IsSupported)
                PhoneDialer.Default.Open(phoneNumber);
        }

        public static async Task SendMailBase(string subject, string body, params string[] emails)
        {
            string[] recipients = emails;
            var message = new EmailMessage
            {
                Subject = subject,
                Body = body,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(recipients)
            };
            await Email.Default.ComposeAsync(message);
        }

        public static async Task GoToAsync(ShellNavigationState state, bool animate = true)
        {
            await Shell.Current.GoToAsync(state, animate);
        }
    }
}
