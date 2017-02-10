/// <binding AfterBuild='default' Clean='clean' />
var gulp = require('gulp');

var plugins = require("gulp-load-plugins")({
  pattern: ['gulp-*', 'gulp.*', 'main-bower-files', 'del'],
  replaceString: /\bgulp[\-.]/
});

gulp.task('clean', function () {
    return plugins.del(['Scripts/**/*', 'Content/site.min.css', 'fonts/**/*']);
});

gulp.task('scripts', function () {    
    return gulp.src(plugins.mainBowerFiles('**/*.js'))
        .pipe(plugins.concat('site.min.js'))
        .pipe(plugins.uglify())
        .pipe(gulp.dest('Scripts/'));
});

gulp.task('styles', function () {
    return gulp.src(['bower_components/bootstrap/dist/css/bootstrap.css', 'Content/*'])
            .pipe(plugins.concat('site.min.css'))
            .pipe(plugins.cleanCss())
            .pipe(gulp.dest('Content/'));
});

gulp.task('fonts', function() {
   return gulp.src(['bower_components/**/*.{eot,svg,ttf,woff,woff2}'])
            .pipe(plugins.flatten())
            .pipe(gulp.dest('fonts/'));
});

gulp.task('build', ['scripts', 'styles', 'fonts']);

// Default task
gulp.task('default', ['clean'], function() {
    gulp.start('build');
});


// Watch
// gulp.watch('Content/site.css', ['styles']);