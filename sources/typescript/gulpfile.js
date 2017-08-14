'use strict';

var gulp = require('gulp');

// load plugins
var $ = require('gulp-load-plugins')();
var tslint = require("gulp-tslint");
var del = require('del');
var runSequence = require('run-sequence');
const argv = require("yargs").argv;
var jeditor = require("gulp-json-editor");
var merge = require('merge2');  // Require separate installation
//var KarmaServer = require('karma').Server;

//set configuration
const tsconfig = require('./tsconfig.json').compilerOptions;

// clean the contents of the distribution directory
gulp.task('clean', function () {
	return del(['.tmp', 'dist/**/*'], {force: true});
});

gulp.task('clean:tmp', function () {
	return del(['.tmp'], {force: true});
});

gulp.task('clean:dist', function () {
	return del(['dist/**/*'], {force: true});
});

gulp.task('scripts:library', function () {
	return compileAppScripts();
});

function compileAppScripts() {
	var tsProject = $.typescript.createProject(tsconfig);
	var opt = {
		tsProject: tsProject,
		inPath: ["src/**/*.ts",
			"!src/tests/**/*.ts",
			"!**/*.spec.ts",
			"!dist/**",
			"!.tmp/**",
			"!node_modules/**"],
		inBase : './src',
		outPath: './.tmp/dist'
	}
	return compileTS(opt);
}

function compileTS(opt) {
	var tsResult = gulp.src(opt.inPath, {base : opt.inBase})
		// .pipe(tslint({
		// 	configuration : 'tslint.json',
		// 	formatter: "prose"
		// }))
		// .pipe(tslint.report({
		// 	emitError: false,
		// 	summarizeFailureOutput : true
		// }))
		.pipe($.sourcemaps.init()) // sourcemaps will be generated
		.pipe(opt.tsProject($.typescript.reporter.fullReporter(true)))
		.on('error', function (error) {
			//if (argv.production) {
				//var log = gutil.log, colors = gutil.colors;
				//log('Typescript compilation exited with ' + colors.red(error));
				process.exit(1);
			//}
		});

	return merge([ // this task is finished when the IO of both operations are done
		tsResult.dts.pipe(gulp.dest(opt.outPath)),
		tsResult.js
			.pipe($.sourcemaps.write()) // sourcemaps are added to the .js file
			.pipe(gulp.dest(opt.outPath))
	]);
}

gulp.task('extras', function () {
	var packageFileResult = gulp.src(['package.json'], {base: './'})
		.pipe(jeditor(function(json) {
			json.peerDependencies = json.dependencies;
			json.dependencies = {};
			json.devDependencies = {};
			json.scripts = {};
			return json; // must return JSON object.
		}))
		.pipe(gulp.dest('./.tmp/dist'));

	var extraResult = gulp.src(['./LICENSE','./.gitignore','./README.md','.npmignore'], {base: './'}).pipe(gulp.dest('./.tmp/dist'));


	return merge([
		extraResult,
		packageFileResult
	])
});

gulp.task('copyTmpToDist', ['clean:dist'], function () {
	return gulp.src(['./.tmp/dist/**/*','./.tmp/dist/**/.*'], {base: './.tmp/dist/'}).pipe(gulp.dest('./dist'));
});

gulp.task('build',function () {
	return runSequence('clean:tmp','scripts:library','extras','copyTmpToDist','clean:tmp');
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