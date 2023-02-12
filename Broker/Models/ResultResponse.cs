using Microsoft.AspNetCore.Mvc;

namespace BrokerApi.Models
{
    public class ResultResponse<T> 
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
