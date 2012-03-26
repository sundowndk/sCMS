// Delay before executing asyncronis request.
_asyncdelay : 10,

new : function (rootid, templateid, title)
{	
	var content = new Array ();
	content["rootid"] = rootid;
	content["templateid"] = templateid;
	content["title"] = title;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.New", "data", "POST", false);		
	request.send (content);

	return request.respons ()["scms.page"];
},
	
load : function (id)
{
	var content = new Array ();
	content["id"] = id;
	
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.Load", "data", "POST", false);	
	request.send (content);

	return request.respons ()["scms.page"];
},

save : function (page)
{	
	var content = new Array ();
	content["scms.page"] = page;
								
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.Save", "data", "POST", false);			
	request.send (content);

	return true;
},

delete : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.Delete", "data", "POST", false);	
	request.send (content);
					
	return true;					
},

list : function (attributes)
{
	if (!attributes) attributes = new Array ();
	
	if (attributes.async)
	{
		var onDone = 	function (respons)
						{
							attributes.onDone (respons["scms.pages"]);
						};
						
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.List", "data", "POST", true);
		request.onLoaded (onDone);
		request.send ();
	}
	else
	{
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.List", "data", "POST", false);		
		request.send ();

		return request.respons ()["scms.pages"];	
	}
},



listold : function (options)
{
	if (options == null) 
	{
		options = new Array ();
	}

	if (options.async)
	{
		var rootsdone = false;
		var pagesdone = false;
		var result = new Array ();
		
		var onroots = 	function (roots) 
				{
					for (index in roots)
					{
						roots[index]["type"] = "root";
						result[result.length] = roots[index];
					}		
					rootsdone = true;			
				};
				
		var onpages = 	function (data)
				{
					var pages = data["pages"];
					for (index in pages)
					{
						pages[index]["type"] = "page";					
						result[result.length] = pages[index];
					}	
					pagesdone = true;				
				};
				
		var ondone = 	function ()
				{
					if (rootsdone && pagesdone)
					{
						options.onDone (result);
					}
					else
					{
						setTimeout (ondone, 100);
					}
				};
				
		setTimeout (ondone, 100);
				
		sCMS.root.list ({async: true, onDone: onroots});
							
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.List", "data", "POST", true);
		request.onLoaded (onpages);
		request.send ();				
	}
	else
	{	
		var result = new Array ();
	
		var roots = sCMS.root.list ();
		for (index in roots)
		{
			roots[index]["type"] = "root";
			result[result.length] = roots[index];
		}

		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.List", "data", "POST", false);		
		request.send ();

		var pages = request.respons ()["pages"];	
		for (index in pages)
		{
			pages[index]["type"] = "page";					
			result[result.length] = pages[index];
		}
		
		return result;				
	}

	
//	
	

//					// Get pages.
//					var onpagesloaded = 	function (pages)	
//								{ 
//									for (index in pages)
//									{
//										pages[index]["type"] = "page";
//										UI.elements.listview["pages"].addItem (pages[index]);
//									}												
//								};
								
//					var pages = sCMS.page.list ({async: true, ondone: onpagesloaded});
					
//					for (index in pages)
//					{
//						pages[index]["type"] = "page";					
//						tree[tree.length] = pages[index];
//					}				
				
					// Add to listview.
//					UI.elements.listview["pages"].setItems (tree);

//	if (options.async)
//	{
//		var ondone = function (data)
//		{
//			options.ondone (data["pages"]);
//		}
//		
//		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.List", "data", "POST", true);		
//		request.onLoaded (ondone);
//		request.send ();
//	}
//	else
//	{
//		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.List", "data", "POST", false);		
//		request.send ();
//		return request.respons ()["pages"];	
//	}

}		
