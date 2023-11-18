using AnimeShopping.Web.Models;
using AnimeShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AnimeShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProducts();

            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                //var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProduct(model);
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }

            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            //var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindProductById(id);
            if (product != null)
                return View(product);
            return NotFound();
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
               // var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProduct(model);
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }

            return View(model);
        }

        //[Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            //var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindProductById(id);
            if (product != null)
                return View(product);
            return NotFound();
        }

        [HttpPost]
        //[Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            //var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(model.Id);
            if (response)
                return RedirectToAction(nameof(ProductIndex));

            return View(model);
        }
    }
}
