using System.Net;

namespace CoffeeHouse.Core.Misc
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class GeneralResponse<T> : GeneralResponse
    {
        public T Content { get; set; }
    }
}
