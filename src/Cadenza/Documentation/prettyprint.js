var keywords = "";

var keywordArray = new Array (
"abstract", "event",    "new",      "struct",    "as",         "explicit", 
"null",     "switch",   "base",     "extern",    "object",     "this", 
"bool",     "false",    "operator", "throw",     "break",      "finally", 
"out",      "true",     "byte",     "fixed",     "override",   "try",
"case",     "float",    "params",   "typeof",    "catch",      "for",
"private",  "uint",     "char",     "foreach",   "protected",  "ulong",
"checked",  "goto",     "public",   "unchecked", "class",      "if",
"readonly", "unsafe",   "const",    "implicit",  "ref",        "ushort",
"continue", "in",       "return",   "using",     "decimal",    "int",
"sbyte",    "virtual",  "default",  "interface", "sealed",     "volatile",
"delegate", "internal", "short",    "void",      "do",         "is",
"sizeof",   "while",    "double",   "lock",      "stackalloc", "else",
"long",     "static",   "enum",     "namespace",  "string"
);


function paintColors()
{
	keywords = "(";
	for(n = 0; n < keywordArray.length; n++)
		keywords += "\\b" + keywordArray[n] + "\\b|";
	keywords += "string)";
	
	var elems = document.getElementsByTagName("xmp");
	paintColorsForTags (elems);

	elems = document.getElementsByTagName ("pre");
	paintColorsForTags (elems);
}

function paintColorsForTags (elems)
{
	for(n = elems.length - 1; n >= 0; n--) {
		if (elems[n].className == "code-csharp")
			format(elems [n], formatCs);
		else if (elems[n].className == "code-xml")
			format(elems [n], formatXml);
		else if (elems[n].className == "code-mdb")
			format(elems [n], formatMdb);
		else if (elems[n].className == "code-gaim")
			format(elems [n], formatGaim);
	}
}

function format(node, func)
{
	text = node.innerHTML;
	div = document.createElement("div");
	//var className = node.className;
	var className = "formattedcode";
	
	// remove trailing/leading lines
	while(text.charAt(0) == "\n" || text.charAt(0) == "\r")
		text = text.substr(1);
	
	while(text.charAt(text.length) == "\n" || 
		text.charAt(text.length) == "\r")
		text = text.splice(0, -1);

	div.innerHTML = func(text);
	node.parentNode.replaceChild(div, node);
	div.className = className;
}

function formatCs(text)
{
	var re = / /g;
	text = text.replace(re, "&nbsp;");
	re = /<(.*?)>/g;
	text = text.replace(re, "<___span style='color:red'_!_$1___/span_!_>");
	re = /</g;
	text = text.replace(re, "&lt;");
	re = />/g;
	text = text.replace(re, "&gt;");
	re = /___/g;
	text = text.replace (re, "<");
	re = /_!_/g;
	text = text.replace (re, ">");

	// cant get this one to work in the good syntax
	re = new RegExp("\"((((?!\").)|\\\")*?)\"","g");
	text = text.replace(re,"<span style='color:purple'>\"$1\"</span>");
	re = /\/\/(((.(?!\"\<\/span\>))|"(((?!").)*)"\<\/span\>)*)((\r|\n|\r\n)|$)/g;
	text = text.replace(re,"<span style='color:green'>//$1</span><br/>");
	re = new RegExp (keywords,"g");
	text = text.replace(re,"<span style='color:blue'>$1</span>");
	re = /\t/g;
	text = text.replace(re,"&nbsp;&nbsp;&nbsp;&nbsp;");
	re = /\n/g;
	text = text.replace (re,"<br/>");
	div = document.createElement("pre");
	div.innerHTML = text;
	spans = div.getElementsByTagName ("span")
	for(i = 0; i < spans.length; i++) {
		if (spans [i].parentNode.nodeName == "SPAN") {
			spans [i].style.color = "";
		}
	}
	
	return div.innerHTML;
}

function formatXml (text)
{	
	var re = / /g;
	text = text.replace (re, "&nbsp;");

	re = /\t/g;
	text = text.replace (re, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
	
	re = /\<\s*(\/?)\s*(.*?)\s*(\/?)\s*\>/g;
	text = text.replace (re,"{blue:&lt;$1}{maroon:$2}{blue:$3&gt;}");
	
	re = /{(\w*):(.*?)}/g;
	text = text.replace (re,"<span style='color:$1'>$2</span>");

	re = /"(.*?)"/g;
	text = text.replace (re,"\"<span style='color:purple'>$1</span>\"");

	re = /\r\n|\r|\n/g;
	text = text.replace (re, "<br/>");
	
	return text;
}

function formatMdb (text)
{	
	var re = / /g;
	text = text.replace (re, "&nbsp;");

	re = /\t/g;
	text = text.replace (re, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
	
	re = /^\$(.*)/g;
	text = text.replace (re, "{blue:\$}{maroon:$1}");

	re = /\(mdb\)(.*)/g
	text = text.replace (re,"{blue:(mdb)}{maroon:$1}");

	re = /{(\w*):(.*?)}/g;
	text = text.replace (re,"<span style='color:$1'>$2</span>");

	re = /\r\n|\r|\n/g;
	text = text.replace (re, "<br/>");
	
	return text;
}

function formatGaim (text)
{
	var re = / /g;
	text = text.replace (re, "&nbsp;");

	re = /\t/g;
	text = text.replace (re, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

	re = /\(([0-9]+:[0-9]+:[0-9]+)\)?(.*?):\s*(.*?)/g
	text = text.replace (re,"{blue:(}{maroon:$1}{blue:)}{red:$2}:$3");

	re = /{(\w*):(.*?)}/g;
	text = text.replace (re,"<span style='color:$1'>$2</span>");
	
	re = /\r\n|\r|\n/g;
	text = text.replace (re, "<br/>");
	
	return text;
}

window.onload = function () {
	paintColors();
}

