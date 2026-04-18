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
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecuter.InvokePost<Shirt>("shirts/createshirtwithbody", shirt);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (WebApiException ex)
                {
                    HandleWebApiException(ex);
                }
            }
            return View(shirt);
        }

        #endregion

        #region Edit Shirt

        public async Task<IActionResult> EditShirt(int shirtId)
        {
            try
            {
                var shirt = await webApiExecuter.InvokeGet<Shirt>($"shirts/{shirtId}");

                if (shirt != null)
                    return View(shirt);
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditShirt(Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecuter.InvokePut<Shirt>($"shirts", shirt);
                    return RedirectToAction(nameof(Index));
                }
                catch (WebApiException ex)
                {
                    HandleWebApiException(ex);
                }
            }
            return View(shirt);
        }

        #endregion

        #region Remove Shirt

        [HttpPost]
        public async Task<IActionResult> RemoveShirt(int shirtId)
        {
            try
            {
                await webApiExecuter.InvokeDelete($"shirts/{shirtId}");
                return RedirectToAction(nameof(Index));
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
                return View(nameof(Index), await webApiExecuter.InvokeGet<List<Shirt>>("shirts"));
            }
        }

        #endregion

        #region Helpers

        private void HandleWebApiException(WebApiException ex)
        {
            if (ex.ErrorResponse != null && ex.ErrorResponse.Errors != null && ex.ErrorResponse.Errors.Count > 0)
            {
                foreach (var error in ex.ErrorResponse.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join("; ", error.Value));
                }
            }
        }

        #endregion
    }
}
