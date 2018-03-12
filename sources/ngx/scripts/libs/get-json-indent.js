// credits to https://github.com/mapbox/detect-json-indent/commit/491b5f87d59091dd266cc6fe6430b26b436a33ad

module.exports = getJsonIndent;

function getJsonIndent(_) {
	if (_ === '{}') return '    ';
	var lines = _.split('\n');
	if (lines.length < 2) return null;
	var space = lines[1].match(/^(\s*)/);
	return space[0];
};