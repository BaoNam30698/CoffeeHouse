namespace CoffeeHouse.App.Models.Login
{
    public class UserAuthentication
    {
        public bool CanLogin { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
