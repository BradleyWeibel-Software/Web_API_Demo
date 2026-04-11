using Microsoft.AspNetCore.Mvc;
using Web_API_Demo.Data;
using Web_API_Demo.Repositories;

namespace WebApp.Controllers
{
    public class ShirtsController : Controller
    {
        private readonly ShirtRepository shirtRepository;

        public ShirtsController(ApplicationDBContext dbContext)
        {
            shirtRepository = new ShirtRepository(dbContext);
        }

        public IActionResult Index()
        {
            var shirts = shirtRepository.GetShirts();
            return View(shirts);
        }
    }
}
