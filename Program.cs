using System.Net;
using App.ExtendMethods;
using App.Services;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // Mặc định :  /Views/Controller/Action.cshtml
    // Thêm     :  /MyViews/Controller/Action.cshtml

    // {0} -> Tên Action
    // {1} -> Tên Controller
    // {2} -> Tên Area

    //RazorViewEngine.ViewExtension = .cshtml
    options.ViewLocationFormats.Add("/MyViews/{1}/{0}" + RazorViewEngine.ViewExtension);
});

// Các cách viết tương tự nhau
// builder.Services.AddSingleton<ProductService>();
// builder.Services.AddSingleton<ProductService, ProductService>();
// builder.Services.AddSingleton(typeof(ProductService));
builder.Services.AddSingleton(typeof(ProductService), typeof(ProductService));
builder.Services.AddSingleton<PlanetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware -> Lỗi code 400 đến 599
app.AddStatusCodePage(); // Sử dụng ExtendMethods tùy biến

app.UseRouting();

app.UseAuthentication(); // Xác định danh tính
app.UseAuthorization();  // Xác định quyền truy cập

// Routing
app.UseEndpoints(enpoints => {
    enpoints.MapGet("/sayhi", async context => {
        await context.Response.WriteAsync($"ASP.NET MVC: {DateTime.Now}");
    });
});

// Area
app.MapAreaControllerRoute(
    name: "product",
    pattern: "{controller}/{action=Index}/{id?}",
    areaName: "ProductManage");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapControllers();
// app.MapControllerRoute();
// app.MapDefaultControllerRoute
// app.MapAreaControllerRoute();

// URL = start-here
// Controller =>
// Action =>
// Area =>
// app.MapControllerRoute(
//     name: "firstRoute",
//     pattern: "start-here/{controller}/{action}/{id?}", // start-here, start-here/1, start-here/123
//     defaults: new {
        // controller = "First",
        // action = "ViewProduct",
        // id = 3
//     }
// );
 app.MapRazorPages();

app.Run();
