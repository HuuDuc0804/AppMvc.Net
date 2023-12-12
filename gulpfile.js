var gulp = require("gulp"),
  cssmin = require("gulp-cssmin");
rename = require("gulp-rename");
const sass = require("gulp-sass")(require("sass"));

gulp.task("default", function () {
  return gulp
    .src("assets/scss/site.scss")
    .pipe(sass().on("error", sass.logError))
    .pipe(cssmin()) // Loại bỏ kí tự thừa
    .pipe(
      rename({
        // suffix: ".min" // Đặt tên có hậu tố min
      })
    )
    .pipe(gulp.dest("wwwroot/css/"));
});
gulp.task("watch", function () {
    gulp.watch('assets/scss/*.scss', gulp.series("default"));
});