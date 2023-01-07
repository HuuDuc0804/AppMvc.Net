using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [Area("ProductManage")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productService;

        public ProductController(ILogger<ProductController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        public IActionResult Index()
        {
            // /Areas/AreaName/Views/ControllerName/Action.cshtml
            var products = _productService.OrderBy(p => p.Name).ToList();
            return View(products); // /Areas/ProductManage/Views/Product/Index.cshtml
        }
    }
}