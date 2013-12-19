
//返回对象的滚动条的偏移量，默认是浏览器窗口的
function getScrollOffsets(w){
	w = w||window;
	//除了ie8及之前的版本外，所有浏览器都能用
	if (w.pageXOffset!=null) {
		return {x:w.pageXOffset,y:w.pageYOffset}
	};

	//对标准模式的ie
	var d = w.document;
	if (document.compatMode=="CSS1Compat") {
		return {x:d.documentElement.scrollLeft,y:d.documentElement.scrollTop};
	};

	//对怪异模式下的浏览器
	return { x:d.body.scrollLeft,y:d.body.scrollTop};
}

//查询一个窗口的视口尺寸
function getViewportSize(w){

	w = w || window;
	//除了ie8即之前的版本外，其他浏览器都能用
	if (w.innerWidth != null) { return {w:w.innerWidth,h:w.innerHeight}};

		//对标准模式的ie
	var d = w.document;
	if (document.compatMode=="CSS1Compat") {
		return {w: d.documentElement.clientWidth,h:d.documentElement.clientHeight};
	};
	//对怪异模式下的浏览器
	return　{w:d.body.clientWidth,h:d.body.clientHeight};
}