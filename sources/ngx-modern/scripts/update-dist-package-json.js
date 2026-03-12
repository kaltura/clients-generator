const fs = require('fs');
const path = require('path');

const distPackageJson = path.resolve(__dirname, '../dist/kaltura-ngx-client/package.json');

function formatTwoDigitsNumber(value) {
  return ("0" + value).slice(-2);
}

function updateDistPackageJson() {
  const packageContent = JSON.parse(fs.readFileSync(distPackageJson, 'utf8'));

  const now = new Date();
  packageContent.version = packageContent.version + '-v' + now.getFullYear() + formatTwoDigitsNumber(now.getMonth() + 1) + formatTwoDigitsNumber(now.getDate()) + '-' + formatTwoDigitsNumber(now.getHours()) + formatTwoDigitsNumber(now.getMinutes()) + formatTwoDigitsNumber(now.getSeconds());
  packageContent.devDependencies = {};
  packageContent.peerDependencies = packageContent.dependencies || {};
  packageContent.dependencies = {};
  packageContent.scripts = {};
  delete packageContent.jest;
  packageContent.private = true; // IMPORTANT - this library uses a name that is reserved and shouldn't be published to NPM repository. Before publishing it make sure you change its name to something else

  if (packageContent.config && packageContent.config.npmDistDirectory) {
    delete packageContent.config.npmDistDirectory;
  }

  fs.writeFileSync(distPackageJson, JSON.stringify(packageContent, null, 2), 'utf8');
  console.log('Updated dist package.json with version:', packageContent.version);
}

updateDistPackageJson();
