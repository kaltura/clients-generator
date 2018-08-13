const path = require('path');
const loadJsonFile = require("load-json-file");
const writeJsonFile = require("write-json-file");

const distPackageJson = path.resolve(__dirname,'../dist/kaltura-ngx-client/package.json');

function formatTwoDigitsNumber(value)
{
  return ("0" + value).slice(-2);
}

function updateDistPackageJson() {

  const packageContent = loadJsonFile.sync(distPackageJson);

  var now = new Date();
  packageContent.version = packageContent.version + '-v' + now.getFullYear() + formatTwoDigitsNumber(now.getMonth() + 1) + formatTwoDigitsNumber(now.getDate()) + '-' + formatTwoDigitsNumber(now.getHours()) + formatTwoDigitsNumber(now.getMinutes()) + formatTwoDigitsNumber(now.getSeconds());
  packageContent.devDependencies = {};
  packageContent.peerDependencies = packageContent.dependencies;
  packageContent.dependencies = {};
  packageContent.scripts = {};
  delete packageContent.jest;
  packageContent.private = true; // IMPORTANT - this library uses a name that is reserved and shouldn't be publish to NPM respository. before publishing it make sure you change its' name to something else

  if (packageContent.config && packageContent.config.npmDistDirectory) {
    delete packageContent.config.npmDistDirectory;
  }

  writeJsonFile.sync(distPackageJson, packageContent);
}

updateDistPackageJson();
