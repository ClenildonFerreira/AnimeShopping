﻿using AnimeShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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
    }
}