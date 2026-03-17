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
        public List<Shirt> GetShirts()
        {
            return shirts;
        }

        // GET : https://localhost:7104/api/shirts/1
        [HttpGet("{id}")]
        public IActionResult GetShirtById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = shirts.FirstOrDefault(s => s.Id == id);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        // GET : https://localhost:7104/api/shirts/1/blue
        [HttpGet("{id}/{colour}")]
        public IActionResult GetShirtByIdAndColour(int id, [FromRoute]string colour)
        {
            return Ok($"Reading shirt: {id} with colour: '{colour}'");
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
                $"sex: {shirtData.Sex}, \n" +
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

        #region Helpers

        private List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt() { Id = 1, Brand = "Nike", Colour = "Black", Size = 10, Sex = "Female" },
            new Shirt() { Id = 2, Brand = "Puma", Colour = "Red", Size = 12, Sex = "Male" },
            new Shirt() { Id = 3, Brand = "RHCP", Colour = "Grey", Size = 10, Sex = "Female" },
            new Shirt() { Id = 4, Brand = "TBK", Colour = "Pink", Size = 6, Sex = "Female" },
            new Shirt() { Id = 5, Brand = "TBS", Colour = "Black", Size = 8, Sex = "Male" },
            new Shirt() { Id = 6, Brand = "DS", Colour = "White", Size = 11, Sex = "Male" },
            new Shirt() { Id = 7, Brand = "Adidas", Colour = "Grey", Size = 10, Sex = "Female" },
            new Shirt() { Id = 8, Brand = "[no-name]", Colour = "White", Size = 12, Sex = "Male" },
            new Shirt() { Id = 9, Brand = "Nike", Colour = "Gold", Size = 10, Sex = "Female" },
            new Shirt() { Id = 10, Brand = "RHCP", Colour = "Red", Size = 10, Sex = "Female" }
        };

        #endregion
    }
}
