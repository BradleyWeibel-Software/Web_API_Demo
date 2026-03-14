using Microsoft.AspNetCore.Mvc;

namespace Web_API_Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        // GET: https://localhost:7104/api/shirts
        [HttpGet]
        public string GetShirts()
        {
            return "Reading all shirts";
        }

        // GET : https://localhost:7104/api/shirts/1
        [HttpGet("{id}")]
        public string GetShirtById(int id)
        {
            return $"Reading shirt: {id}";
        }

        // POST: https://localhost:7104/api/shirts
        [HttpPost]
        public string CreateShirt()
        {
            return $"Creating a shirt";
        }

        // PUT: https://localhost:7104/api/shirts/1
        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Updating shirt: {id}";
        }

        // DELETE: https://localhost:7104/api/shirts/1
        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Deleting shirt: {id}";
        }
    }
}
