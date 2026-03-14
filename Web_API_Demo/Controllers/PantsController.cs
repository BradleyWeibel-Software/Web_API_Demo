using Microsoft.AspNetCore.Mvc;

namespace Web_API_Demo.Controllers
{
    [ApiController]
    public class PantsController : ControllerBase
    {
        public string GetPants()
        {
            return "Reading all pants";
        }

        public string GetPanstById(int id)
        {
            return $"Reading pants: {id}";
        }

        public string CreatePants()
        {
            return $"Creating a pants";
        }

        public string UpdatePants(int id)
        {
            return $"Updating pants: {id}";
        }

        public string DeletePants(int id)
        {
            return $"Deleting pants: {id}";
        }
    }
}
