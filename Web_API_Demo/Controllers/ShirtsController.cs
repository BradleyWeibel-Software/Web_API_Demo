using Microsoft.AspNetCore.Mvc;

namespace Web_API_Demo.Controllers
{
    [ApiController]
    public class ShirtsController : ControllerBase
    {
        public string GetShirts()
        {
            return "Reading all shirts";
        }

        public string GetShirtById(int id)
        {
            return $"Reading shirt: {id}";
        }

        public string CreateShirt()
        {
            return $"Creating a shirt";
        }

        public string UpdateShirt(int id)
        {
            return $"Updating shirt: {id}";
        }

        public string DeleteShirt(int id)
        {
            return $"Deleting shirt: {id}";
        }
    }
}
