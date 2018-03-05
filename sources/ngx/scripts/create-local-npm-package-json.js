
const getJsonIndent = require('./libs/get-json-indent');

module.exports = createLocalNpmPackageJson;

function createLocalNpmPackageJson(packageContentAsString) {

  if (packageContentAsString) {

    var packageIndent = getJsonIndent(packageContentAsString) || 4;
    var packageContent = JSON.parse(packageContentAsString);

    var now = new Date();
    packageContent.version = packageContent.version + '-v' + now.getFullYear() + ("0" + (now.getMonth() + 1)).slice(-2) + ("0" + now.getDate()).slice(-2) + '-' + now.getHours() + now.getMinutes() + now.getSeconds();
    packageContent.devDependencies = {};
    packageContent.peerDependencies = packageContent.dependencies;
    packageContent.dependencies = {};
    packageContent.scripts = {};
    packageContent.private =  true; // IMPORTANT - this library uses a name that is reserved and shouldn't be publish to NPM respository. before publishing it make sure you change its' name to something else

    if (packageContent.config && packageContent.config.npmDistDirectory) {
      delete packageContent.config.npmDistDirectory;
    }

    return JSON.stringify(packageContent,null,packageIndent);
  }
}