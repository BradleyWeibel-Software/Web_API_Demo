using Microsoft.AspNetCore.Mvc;
using Web_API_Demo.Data;
using Web_API_Demo.Model;
using Web_API_Demo.Repositories;

namespace Web_API_Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        private readonly ShirtRepository shirtRepository;

        public ShirtsController(ApplicationDBContext dbContext)
        {
            shirtRepository = new ShirtRepository(dbContext);
        }

        #region GET

        // GET: https://localhost:7104/api/shirts
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(shirtRepository.GetShirts());
        }

        // GET : https://localhost:7104/api/shirts/1
        [HttpGet("{id}")]
        public IActionResult GetShirtById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = shirtRepository.GetShirtById(id);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        // GET : https://localhost:7104/api/shirts/1/blue
        [HttpGet("{id}/{colour}")]
        public IActionResult GetShirtByIdAndColour(int id, [FromRoute] string colour)
        {
            return Ok($"Reading shirt: {id} with colour: '{colour}'");
        }

        // GET : https://localhost:7104/api/shirts/getshirt/1?colour=blue&size=large
        [HttpGet("getshirt/{id}")]
        public string GetShirtByIdAndPassInfo(int id, [FromQuery] string colour, [FromQuery] string size)
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
        public IActionResult AddShirt([FromForm] Shirt newShirt)
        {
            try
            {
                if (newShirt == null)
                    return BadRequest("Shirt contains no data");

                var shirtAlreadyExists = shirtRepository.ShirtExists(newShirt.Id ?? 0);
                if (shirtAlreadyExists)
                    return BadRequest($"Shirt Id '{newShirt.Id}' already exists");

                shirtAlreadyExists = shirtRepository.GetShirtByProperties(newShirt) != null;
                if (shirtAlreadyExists)
                    return BadRequest("Shirt with these characteristics already exists");

                var newShirtId = shirtRepository.AddShirt(newShirt);
                if (newShirtId == null)
                    return BadRequest("Shirt creation was unsuccessful");
                else
                    return CreatedAtAction(nameof(shirtRepository.GetShirtById), new { Id = newShirt }, newShirt);
            }
            catch (Exception)
            {
                return BadRequest("Shirt creation was unsuccessful");
            }
        }

        // POST: https://localhost:7104/api/shirts/createshirtwithbody
        [HttpPost("createshirtwithbody")]
        public IActionResult CreateShirtWithBody([FromBody] Shirt newShirt)
        {
            try
            {
                if (newShirt == null)
                    return BadRequest("Shirt contains no data");

                var shirtAlreadyExists = shirtRepository.ShirtExists(newShirt.Id ?? 0);
                if (shirtAlreadyExists)
                    return BadRequest($"Shirt Id '{newShirt.Id}' already exists");

                shirtAlreadyExists = shirtRepository.GetShirtByProperties(newShirt) != null;
                if (shirtAlreadyExists)
                    return BadRequest("Shirt with these characteristics already exists");

                var newShirtId = shirtRepository.AddShirt(newShirt);
                if (newShirtId == null)
                    return BadRequest("Shirt creation was unsuccessful");
                else
                    return CreatedAtAction(nameof(shirtRepository.GetShirtById), new { Id = newShirt }, newShirt);
            }
            catch (Exception)
            {
                return BadRequest("Shirt creation was unsuccessful");
            }
        }

        #endregion

        #region PUT, Patch

        // PUT: https://localhost:7104/api/shirts
        [HttpPut]
        public IActionResult UpdateShirt([FromBody]Shirt updateShirt)
        {
            try
            {
                if (updateShirt == null)
                    return BadRequest("Shirt contains no data");

                if (updateShirt.Id == null || updateShirt.Id <= 0)
                    return BadRequest("Shirt Id is out of bounds");

                var shirtExists = shirtRepository.ShirtExists((int)updateShirt.Id);
                if (shirtExists)
                {
                    var result = shirtRepository.UpdateShirt(updateShirt);
                    if (result)
                        return Ok($"Shirt with Id '{updateShirt.Id}' successfully updated");
                    else
                        return BadRequest($"Shirt update was unsuccessful");
                }
                else
                    return BadRequest($"Shirt Id '{updateShirt.Id}' does not exist");
            }
            catch (Exception)
            {
                return BadRequest($"Shirt update was unsuccessful");
            }
        }

        #endregion

        #region DELETE

        // DELETE: https://localhost:7104/api/shirts/1
        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Shirt Id is out of bounds");

                var shirtExists = shirtRepository.ShirtExists(id);
                if (shirtExists)
                {
                    var result = shirtRepository.DeleteShirt(id);
                    if (result)
                        return Ok($"Shirt with Id '{id}' successfully deleted");
                    else
                        return BadRequest("Shirt deletion was unsuccessful");
                }
                else
                    return BadRequest($"Shirt Id '{id}' does not exist");
            }
            catch (Exception)
            {
                return BadRequest("Shirt deletion was unsuccessful");
            }
        }

        #endregion

        #region Helpers

        

        #endregion
    }
}
