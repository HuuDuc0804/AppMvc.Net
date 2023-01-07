using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ProductService _productService;

        public FirstController(ILogger<FirstController> logger, IWebHostEnvironment env,
        ProductService productService)
        {
            _logger = logger;
            _env = env;
            _productService = productService;
        }
        public string Index()
        {
            // this.HttpContext
            // this.Request
            // this.Response
            // this.RouteData

            // this.User
            // this.ModelState
            // this.ViewData
            // this.ViewBag
            // this.Url
            // this.TempData
            _logger.LogInformation("Truy cập index first");
            return "Index First";
        }

        // Trả về object hoặc bất cứ gì
        public object Any() => DateTime.Now;

        // Trả về content
        public IActionResult Readme()
        {
            string content = @"
                xin chào

                Đây là chuỗi trả về từ content
                Khóa học ASP MVC
            ";
            return Content(content, "text/plain");
        }

        // Trả về hình ảnh
        public IActionResult Picture()
        {
            string filePath = Path.Combine(_env.ContentRootPath, "Files", "index.png");
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "image/png");
        }
        // Trả về Json
        public IActionResult IphonePrice()
        {
            return Json(new
            {
                productName = "Iphone",
                price = 50000
            });
        }

        // Trả về LocalRedirect
        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy", "Home");
            _logger.LogInformation("Chuyển hướng đến: " + url);
            return LocalRedirect(url!);
        }

        // Trả về Redirect
        public IActionResult Google()
        {
            var url = "https://google.com";
            _logger.LogInformation("Chuyển hướng đến: " + url);
            return Redirect(url);
        }

        public IActionResult HelloView(string username)
        {
            if (string.IsNullOrEmpty(username))
                username = "Khách";
            // View() -> Razor Engine, đọc file .cshtml (template)
            //=======================
            // View(template) - template là đường dẫn tuyệt đối đến file .cshtml
            // return View("/MyViews/HelloWorld.cshtml");
            // View(template, model)
            // return View("/MyViews/HelloWorld.cshtml", username);

            // HelloWorld.cshtml -> /Views/First/HelloWorld.cshtml
            // return View("HelloWorld", username);

            // HelloView.cshtml -> /Views/First/HelloView.cshtml 
            // return View(); // (trùng tên action controller)
            // return View((object)username);

            // Mặc định /Views/Controller/Action.cshtml
            return View("HelloWorldCustom", username);

        }

        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
                return NotFound("Không tìm thấy sản phẩm");
            // /Views/First/ViewProduct.cshtml
            // /MyViews/First/ViewProduct.cshtml
            return View(product);
        }
        public IActionResult ViewProduct2(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
                return NotFound("Không tìm thấy sản phẩm");
            
            this.ViewData["Product"] = product;
            
            return View();
        }
        [TempData]
        public string StatusMessage { get; set; } = string.Empty;
        public IActionResult ViewProduct3(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                // this.TempData["StatusMessage"] = "Không tìm thấy sản phẩm";
                StatusMessage = "Không tìm thấy sản phẩm";
                return Redirect(Url.Action("Index", "Home")!);
            }
            
            this.ViewBag.product = product;
            
            return View();
        }
    }
}