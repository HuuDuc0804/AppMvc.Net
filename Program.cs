using System.Net;
using App.Data;
using App.ExtendMethods;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Email
builder.Services.AddOptions();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IEmailSender, SendMailService>();
// Add DbContext
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("AppMvcConnectitonString")!)
);

//Đăng ký Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login/";
    options.LogoutPath = "/logout/";
    options.AccessDeniedPath = "/khongduoctruycap.html";
});
builder.Services.AddAuthentication()
        .AddGoogle(options =>
        {
            var googleConfig = builder.Configuration.GetSection("Authentication:Google");
            options.ClientId = googleConfig["ClientId"]!;
            options.ClientSecret = googleConfig["ClientSecret"]!;

            // mặc định: https://localhost:7073/signin-google
            options.CallbackPath = "/dang-nhap-google";
        })
        .AddFacebook(options =>
        {
            var facebookConfig = builder.Configuration.GetSection("Authentication:Facebook");
            options.AppId = facebookConfig["AppId"]!;
            options.AppSecret = facebookConfig["AppSecret"]!;
            options.CallbackPath = "/dang-nhap-facebook";

        });

// Đăng ký override Identity Erorr Describer (Custom)
builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();

// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = true;

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewManageMenu", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireRole(RoleName.Administrator);
    });
});
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
app.UseEndpoints(enpoints =>
{
    enpoints.MapGet("/sayhi", async context =>
    {
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
