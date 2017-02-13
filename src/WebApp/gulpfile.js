/// <binding AfterBuild='default' Clean='clean' />
var gulp = require('gulp');
var plugins = require("gulp-load-plugins")({
  pattern: ['gulp-*', 'gulp.*', 'main-bower-files', 'del'],
  replaceString: /\bgulp[\-.]/
});


// configuration
var config = {
    scripts: {
        bundle: 'site.min.js',
        dest: 'Scripts/',
        glob: '**/*.js'
    },
    styles: {
        bundle: 'site.min.css',
        dest: 'Content/',
        src: ['bower_components/bootstrap/dist/css/bootstrap.css', 'Content/*']
    },    
    fonts: {
        dest: 'fonts/',
        src: ['bower_components/**/*.{eot,svg,ttf,woff,woff2}']
    }
}


// tasks
gulp.task('clean', function () {
    return plugins.del(['Scripts/**/*', 'Content/site.min.css', 'fonts/**/*']);
});

gulp.task('scripts', function () {    
    return gulp.src(plugins.mainBowerFiles(config.scripts.glob))
        .pipe(plugins.concat(config.scripts.bundle))
        .pipe(plugins.uglify())
        .pipe(gulp.dest(config.scripts.dest));
});

gulp.task('styles', function () {
    return gulp.src(config.styles.src)
            .pipe(plugins.concat(config.styles.bundle))
            .pipe(plugins.cleanCss())
            .pipe(gulp.dest(config.styles.dest));
});

gulp.task('fonts', function() {
   return gulp.src(config.fonts.src)
            .pipe(plugins.flatten())
            .pipe(gulp.dest(config.fonts.dest));
});

gulp.task('build', ['scripts', 'styles', 'fonts']);

gulp.task('default', ['clean'], function() {
    gulp.start('build');
});

gulp.task('watch', function() {
    gulp.watch('Content/site.css', ['styles']);
});