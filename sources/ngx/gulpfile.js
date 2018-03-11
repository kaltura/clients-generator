'use strict';

var gulp = require('gulp');

// load plugins
var $ = require('gulp-load-plugins')();
var tslint = require("gulp-tslint");
var del = require('del');
var runSequence = require('run-sequence');
var jeditor = require("gulp-json-editor");
const ngc = require('gulp-ngc') ;

var merge = require('merge2');  // Require separate installation

//set configuration
const tsconfig = require('./tsconfig.json').compilerOptions;

const tempDist = './.tmp';
const dist = './dist';

// clean the contents of the distribution directory
gulp.task('clean', ['clean:tmp','clean:dist']);

gulp.task('clean:tmp', function () {
  return del([tempDist], {force: true});
});

gulp.task('clean:dist', function () {
  return del([`${dist}/**/*`], {force: true});
});

gulp.task('library:scripts', function () {
  return ngc('tsconfig.json');
});

gulp.task('extras', function () {
  var packageFileResult = gulp.src(['package.json'], {base: './'})
    .pipe(jeditor(function(json) {
      var now = new Date();
      json.version = `${json.version}-v${now.getFullYear()}${now.getMonth()+1}${now.getDate()}-${now.getHours()}${now.getMinutes()}${now.getSeconds()}`;
      json.peerDependencies = json.dependencies;
      json.dependencies = {};
      json.devDependencies = {};
      json.private =  true; // IMPORTANT - this library name in package.json is general. before publishing it make sure you change its' name
      json.scripts = {};
      return json; // must return JSON object.
    }))
    .pipe(gulp.dest('./.tmp'));

  var extraResult = gulp.src(['./LICENSE','./.gitignore','./README.md','.npmignore'], {base: './'}).pipe(gulp.dest('./.tmp'));


  return merge([
    extraResult,
    packageFileResult
  ])
});

gulp.task('copyTmpToDist', ['clean:dist'], function () {
  return gulp.src([`./${tempDist}/**/*`, `./${tempDist}/**/.*`], {base: tempDist}).pipe(gulp.dest(dist));
});

gulp.task('build',function () {

  return runSequence('clean:tmp','library:scripts','extras','copyTmpToDist','clean:tmp');
});

gulp.task('watch', [], function () {

  runSequence(
    'clean:tmp',
    'build',
    function()
    {
      gulp.watch([
        './src/**/*',
        '!./src/**/*.spec.ts'
      ],debounced('build',3000)).on('change', function (event) {
        console.log('File ' + event.path + ' was ' + event.type);
      });
    }
  )

});

function debounced (task, interval) {
  var rerun = false;
  var running = false;
  var timeout = null;
  console.log(`debounced created with ${interval} timeout dealy`);
  return function debounced () {

    if (!running) {
      rerun = false;

      if (timeout)
      {
        clearTimeout(timeout);
        timeout = null;
      }

      timeout = setTimeout(function () {
        running = true;
        console.log(`Running task '${task}'`);
        timeout = null;
        gulp.start(task,function debounceCallback () {
          running = false;
          if (rerun) {
            console.log(`Another change(s) detected, Re-running task '${task}'`);
            rerun = false;
            gulp.start(task);
          }
        });
      }, interval);
    } else {
      rerun = true;
    }
  };
}