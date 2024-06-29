using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stseniayeva.UI.Data;
using Stseniayeva.UI.Extensions;
using Stseniayeva.UI.Models;
using Stseniayeva.UI.Services;

namespace Stseniayeva.UI.Controllers
{
    public class CartController : Controller
    {
        private AppDbContext _productService;
        private Cart _cart;

        public CartController(IProductService productService)
        {
            _productService = (AppDbContext?)productService;
        }
        // GET: CartController
        public ActionResult Index()
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            return View(_cart.CartItem); 
        }
        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var data = await _productService.GetProductByIdAsync(id);
            if (data.Success)
            {
                _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
                _cart.AddToCart(data.Data);
                HttpContext.Session.Set<Cart>("cart", _cart);
            }
            return Redirect(returnUrl);
        }
        [Route("[controller]/remove/{id:int}")]
        public ActionResult Remove(int id)
        {
            _cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            _cart.RemoveItems(id);
            HttpContext.Session.Set<Cart>("cart", _cart);
            return RedirectToAction("index");
        }
    }
}
