'use strict';

var gulp = require('gulp');

// load plugins
var runSequence = require('run-sequence');
var through = require('through2');
var createLocalNpmPackageJson = require('./scripts/create-local-npm-package-json');

const exec = require('child_process').exec;
var merge = require('merge2');  // Require separate installation

const outDir = './dist';

/**
 * Build ESM by running npm task.
 * This is a temporary solution until ngc is supported --watch mode.
 * @see: https://github.com/angular/angular/issues/12867
 */
gulp.task('build:esm', function (callback)  {
  exec('./node_modules/.bin/ngc -p tsconfig-aot.json', function (error, stdout, stderr) {
    console.log(stdout, stderr);

    if (error)
    {
      process.exit(1);
    }else {
      runSequence('extras',function()
      {
        callback();
    });
    }
  });
});

/**
 * Implements ESM build watch mode.
 * This is a temporary solution until ngc is supported --watch mode.
 * @see: https://github.com/angular/angular/issues/12867
 */
gulp.task('build:esm:watch', ['build:esm'], function() {
  gulp.watch('src/**/*', ['build:esm']);
});


gulp.task('extras', function () {
  var packageFileResult = gulp.src(['package.json'], {base: './'})
    .pipe(through.obj(function (file, enc, cb) {
      if (file)
      {
        const packageFileContent = file.contents.toString('utf8');
        const npmPackageFileContent = createLocalNpmPackageJson(packageFileContent);
        file.contents = new Buffer(npmPackageFileContent);
      }
      cb(null, file)
    }))
    .pipe(gulp.dest(outDir));

  
  var licenseFileResult = gulp.src(['./LICENSE.txt'], {base: '../'}).pipe(gulp.dest(outDir));
  var readmeFileResult = gulp.src(['./README.md'], {base: './'}).pipe(gulp.dest(outDir));

  return merge([
    licenseFileResult,
    packageFileResult,
    readmeFileResult
  ])
});

