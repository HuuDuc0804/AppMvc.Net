## Controller
- Là một lớp kế thừa từ lớp Controller: Microsoft.AspNetCore.Mvc.Controller
- Action trong Controller là một phương thức Public (Không được static)
- Action trả về bất kì kiểu dữ liệu nào, thường là IActionResult
- Các dịch vụ inject vào controller qua hàm tạo
## View
- Là file .cshtml
- View cho Action lưu tại: /View/ControllerName/ActionName.cshtml
- Thêm thư mục lưu trữ View:
```
    // {0} -> Tên Action
    // {1} -> Tên Controller
    // {2} -> Tên Area
    // RazorViewEngine.ViewExtension = .cshtml
    options.ViewLocationFormats.Add("/MyViews/{1}/{0}" + RazorViewEngine.ViewExtension);
```
## Truyền dữ liệu sang View
- Model
- ViewData
- ViewBag
- TempData

## Areas
- Là tên dùng để routing
- Là cấu trúc thư mục chứa M.V.C
- Thiết lập Area cho controller bằng ```[Area("AreaName")]```
- Tạo cấu trúc thư mục
```
dotnet aspnet-codegenerator area Product
```

## Gulp
```
npm init // Sinh ra file package.json
npm install --global gulp-cli // Thêm command line cli

npm install gulp // Cài đặt Gulp
npm install node-sass postcss sass // Cài đặt package

npm install gulp-sass gulp-less gulp-concat gulp-cssmin gulp-uglify rimraf gulp-postcss gulp-rename
```