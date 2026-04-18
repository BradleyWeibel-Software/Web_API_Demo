using System.Text.Json;

namespace WebApp.Data
{
    public class WebApiException : Exception
    {
        public ErrorResponse? ErrorResponse { get; }
        public WebApiException(string errorJson) 
        {
            try
            {
                ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
            }
            catch (JsonException)
            {
                // If deserialization fails, create a basic error response
                ErrorResponse = new ErrorResponse 
                { 
                    Title = "An error occurred",
                    Status = 400,
                    Errors = new() 
                };
            }
        }
    }
}
