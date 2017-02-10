/// <binding AfterBuild='default' Clean='clean' />
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var mainBowerFiles = require('main-bower-files');
var cleanCss = require('gulp-clean-css');
var flatten = require('gulp-flatten');

gulp.task('clean', function () {
    return del(['Scripts/**/*', 'Content/site.min.css', 'fonts/**/*']);
});

gulp.task('scripts', function () {    
    return gulp.src(mainBowerFiles('**/*.js'))
        .pipe(concat('site.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('Scripts/'));
});

gulp.task('styles', function () {
    return gulp.src(['bower_components/bootstrap/dist/css/bootstrap.css', 'Content/*'])
            .pipe(concat('site.min.css'))
            .pipe(cleanCss())
            .pipe(gulp.dest('Content/'));
});

gulp.task('fonts', function() {
   return gulp.src(['bower_components/**/*.{eot,svg,ttf,woff,woff2}'])
            .pipe(flatten())
            .pipe(gulp.dest('fonts/'));
});

gulp.task('build', ['scripts', 'styles', 'fonts']);

// Default task
gulp.task('default', ['clean'], function() {
    gulp.start('build');
});


// Watch
gulp.watch('Content/site.css', ['styles']);