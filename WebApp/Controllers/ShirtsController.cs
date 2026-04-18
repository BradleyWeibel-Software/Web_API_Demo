using Microsoft.AspNetCore.Mvc;
using Web_API_Demo.Model;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class ShirtsController : Controller
    {
        private readonly IWebApiExecuter webApiExecuter;

        public ShirtsController(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        #region Show all shirts

        public async Task<IActionResult> Index()
        {
            return View(await webApiExecuter.InvokeGet<List<Shirt>>("shirts"));
        }

        #endregion

        #region Create Shirt

        public IActionResult CreateShirt()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateShirt(Shirt shirt)
        {
            //if (ModelState.IsValid()) // TODO: Add validation
            //{
            var response = await webApiExecuter.InvokePost<Shirt>("shirts/createshirtwithbody", shirt);
            if (response != null)
                return RedirectToAction(nameof(Index));
            //}
            return View(shirt);
        }

        #endregion

        #region Edit Shirt

        public async Task<IActionResult> EditShirt(int shirtId)
        {
            var shirt = await webApiExecuter.InvokeGet<Shirt>($"shirts/{shirtId}");

            if (shirt != null)
                return View(shirt);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditShirt(Shirt shirt)
        {
            //if (ModelState.IsValid()) // TODO: Add validation
            //{
            await webApiExecuter.InvokePut<Shirt>($"shirts", shirt);
                return RedirectToAction(nameof(Index));
            //}
            // return View(shirt);
        }

        #endregion

        #region Remove Shirt

        [HttpPost]
        public async Task<IActionResult> RemoveShirt(int shirtId)
        {
            await webApiExecuter.InvokeDelete($"shirts/{shirtId}");
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
