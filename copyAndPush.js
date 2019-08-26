const fs = require( 'fs' );
const path = require( 'path' );
const child_process = require( 'child_process' );

const rimraf = require('rimraf');
const copydir = require('copy-dir');



const sourcePath = process.argv[2];
const branch = process.argv[3].replace(/^origin\//, '');
const token = process.argv[4];
const git = process.argv[5] || 'git';



let branchPath = path.join(__dirname, branch);
if(!fs.existsSync(branchPath)) {
	fs.mkdirSync(branchPath);
}

function getPaths(file) {
	return new Promise((resolve, reject) => {
		let generatedPath = path.join(sourcePath, file);
		fs.stat(generatedPath, function(error, stat) {
			if( error ) {
				reject(`Error stating path: ${generatedPath}`, error);
				return;
			}
			
			if(!stat.isDirectory()) {
				reject(`Generated ${generatedPath} is not a directory`);
				return;
			}

			let repo = 'KalturaOttGeneratedAPIClients' + file.substr(0, 1).toUpperCase() + file.substr(1);
			let gitPath = path.join(__dirname, branch, repo);
			
			fs.exists(gitPath, (exists) => {
				if(exists) {
					resolve({
						generatedPath: generatedPath, 
						gitPath: gitPath
					});
				}
				else {
					gitCloneBranch(repo)
					.then(() => {
						resolve({
							generatedPath: generatedPath, 
							gitPath: gitPath
						});
					}, (err) => {
						gitClone(repo)
						.then(() => {
							resolve({
								generatedPath: generatedPath, 
								gitPath: gitPath
							});
						}, (err) => {
                            reject(`Failed to clone repo ${repo}: ${err}`);
                            process.exit(1);
						});
					});
				}
			})
		});
	});
}

function execWithPomise(command, cwd, resolveData, alwaysResolve) {
	if(!resolveData) {
		resolveData = cwd;
	}
	return new Promise((resolve, reject) => {
		child_process.exec(command, {
			cwd: cwd
		}, (err, stdout, stderr) => {
			if(!err) {
				console.log(`Command [${command}] executed successfully in  ${cwd}`);
			}
			if(err && !alwaysResolve) {
				reject(`Failed to execute [${command}] in folder ${cwd}, code [${err.code}]: ${err}`);
			}
			else {
				resolve(resolveData);
			}
		});
	});
}

function gitClone(repo) {
	console.log(`Cloning git repo ${repo} and setting user name`);
	return execWithPomise(`${git} clone https://${token}@github.com/kaltura/${repo} && cd ${repo} && git config user.name "Backend CI" && git config user.email "ott.rnd.core@kaltura.com"`, branchPath);
}

function gitCloneBranch(repo) {
	console.log(`Cloning git repo ${repo}, branch ${branch}`);
	return execWithPomise(`${git} clone -b ${branch} https://${token}@github.com/kaltura/${repo} && cd ${repo} && git config user.name "Backend CI" && git config user.email "ott.rnd.core@kaltura.com"`, branchPath)
}

function gitCheckout(generatedPath, gitPath, isNew) {
	console.log(`Checking out git ${gitPath} branch ${branch}`);
	return execWithPomise(git + ' checkout -B ' + branch, gitPath, {
		generatedPath: generatedPath, 
		gitPath: gitPath
	});
}

function gitPull(generatedPath, gitPath) {
	console.log(`Pulling files to git ${gitPath} branch ${branch}`);
	return execWithPomise(git + ' pull origin ' + branch, gitPath, {
		generatedPath: generatedPath, 
		gitPath: gitPath
	}, true);
}

function copyFiles(generatedPath, gitPath) {
	return new Promise((resolve, reject) => {
		console.log(`Copy from ${generatedPath} to ${gitPath}`);
		
		rimraf(gitPath + '/*', () => {
			copydir(generatedPath, gitPath, (err) => {
				if(err) {
                    reject(`Failed to copy directory from ${generatedPath} to ${gitPath}: ` + err);
                    process.exit(1);
				}
				else {
					resolve(gitPath);
				}
			});
		});
	});
}

function gitAdd(gitPath) {
	console.log(`Adding files to git ${gitPath}`);
	return execWithPomise(git + ' add -A', gitPath);
}

function gitCommit(gitPath) {
	console.log(`Commiting files to git ${gitPath}`);
	return execWithPomise(git + ' commit -m "Auto-generated by Jenkins"', gitPath);
}

function gitPush(gitPath) {
	console.log(`Pushing files to git ${gitPath} branch ${branch}`);
	return execWithPomise(git + ' push origin ' + branch, gitPath);
}

let handledFiles = 0;
function startHandlingFile() {
	handledFiles++;
}

function stopHandlingFile() {
	handledFiles--;
	if(!handledFiles) {
		process.exit(0);
	}
}


fs.readdir(sourcePath, (err, files) => {
	if(err) {
		console.error(`Could not list the directory: ${sourcePath}`, err);
		process.exit(1);
	}
	
	files.forEach((file, index) => {
		startHandlingFile();
		getPaths(file)
		.then(({generatedPath, gitPath}) => {
			return gitCheckout(generatedPath, gitPath);
		})
		.then(({generatedPath, gitPath}) => {
			return gitPull(generatedPath, gitPath);
		})
		.then(({generatedPath, gitPath}) => {
			return copyFiles(generatedPath, gitPath);
		})
		.then((gitPath) => {
			return gitAdd(gitPath);
		})
		.then((gitPath) => {
			return gitCommit(gitPath);
		})
		.then((gitPath) => {
			return gitPush(gitPath);
		})
		.then((gitPath) => {
			stopHandlingFile();
		}, (err) => {
			console.error(err);
			stopHandlingFile();
		});
	});
});
