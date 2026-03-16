using Microsoft.AspNetCore.Mvc;
using Web_API_Demo.Model;

namespace Web_API_Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        #region GET

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

        // GET : https://localhost:7104/api/shirts/1/blue
        [HttpGet("{id}/{colour}")]
        public string GetShirtByIdAndColour(int id, [FromRoute]string colour)
        {
            return $"Reading shirt: {id} with colour: '{colour}'";
        }

        // GET : https://localhost:7104/api/shirts/getshirt/1?colour=blue&size=large
        [HttpGet("getshirt/{id}")]
        public string GetShirtByIdAndPassInfo(int id, [FromQuery]string colour, [FromQuery]string size)
        {
            return $"Reading shirt: {id}, with size '{size}' and colour: '{colour}'";
        }

        // GET : https://localhost:7104/api/shirts/getshirtpassheader/1?colour=blue
        [HttpGet("getshirtpassheader/{id}")]
        public string GetShirtByIdAndHeaderInfo(int id, [FromQuery] string colour, [FromHeader] string size)
        {
            return $"Reading shirt: {id}, with size '{size}' and colour: '{colour}'";
        }

        #endregion

        #region POST

        // POST: https://localhost:7104/api/shirts
        [HttpPost]
        public string CreateShirt()
        {
            return $"Creating a shirt from bupkis I guess";
        }

        // POST: https://localhost:7104/api/shirts/createshirtwithbody
        [HttpPost("createshirtwithbody")]
        public string CreateShirtWithBody([FromBody]Shirt shirtData)
        {
            return $"Creating a shirt with \n" +
                $"id: {shirtData.Id}, \n" +
                $"brand: '{shirtData.Brand}', \n" +
                $"size: {shirtData.Size}, \n" +
                $"colour: {shirtData.Colour}";
        }

        #endregion

        #region PUT, Patch

        // PUT: https://localhost:7104/api/shirts/1
        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Updating shirt: {id}";
        }

        #endregion

        #region DELETE

        // DELETE: https://localhost:7104/api/shirts/1
        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Deleting shirt: {id}";
        }

        #endregion
    }
}
