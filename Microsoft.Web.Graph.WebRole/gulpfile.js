/// <binding BeforeBuild='sass' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');

// Initialize gulp dependencies
var plugins = require("gulp-load-plugins")({
    pattern: ['gulp-*', 'gulp.*'],
    replaceString: /\bgulp[\-.]/
});
// For passing in commandline parameters
var argv = require('yargs').argv;

// Location of files
var paths = {
    css: 'Content/build/css',
    scripts: 'Scripts/build/js'
};

// Lists of files to work with
var sources = {
    sass: 'Content/src/**/*.scss'
};

var onError = function(err) {
    plugins.util.log(plugins.util.colors.red.bold('[ERROR]:'), plugins.util.colors.bgRed(err.message));
}

gulp.task('default', function () {
    // place code for your default task here
});

// Task to run during site deployment. Copies output files to path specified by
// outputFolder commandline parameter
gulp.task('deploy', ['sass'], function() {
    gulp.src(paths.css + "/**/*.css")
        .pipe(gulp.dest(argv.outputFolder));
});

// Create a bundled and minified css from sass files
gulp.task('sass', function () {
    var cssOutputFile = 'msgraph-portal.css';
    gulp.src(sources.sass)
        .pipe(plugins.sass())
        .on('error', onError)
        .pipe(plugins.concat(cssOutputFile))
        .pipe(plugins.cleanCss({ compatibility: 'ie8' }))
        .pipe(gulp.dest(paths.css));
});

gulp.task('watch', function() {
    gulp.watch(sources.sass, ['sass']);
});
