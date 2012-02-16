// ---------------------------------------------------------------------------------------------------------------
// PROJECT: scms
// ---------------------------------------------------------------------------------------------------------------
// ---------------------------------------------------------------------------------------------------------------
// CLASS: sCMS
// ---------------------------------------------------------------------------------------------------------------
var sCMS =
{
	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: root
	// ---------------------------------------------------------------------------------------------------------------
	root :
	{
		// Delay before executing asyncronis request.
		_asyncdelay : 10,
		
		new : function (title)
		{
			var content = new Array ();
			content["title"] = title;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.New", "data", "POST", false);			
			request.send (content);
		
			return request.respons ()["scms.root"];
		},
			
		load : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.Load", "data", "POST", false);	
			request.send (content);
		
			return request.respons ()["scms.root"];
		},
		
		save : function (root)
		{					
			var content = new Array ();
			content["scms.root"] = root;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.Save", "data", "POST", false);		
			request.send (content);
		
			return true;
		},
		
		delete : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.Delete", "data", "POST", false);	
			request.send (content);
							
			return true;					
		},
		
		list : function (options)
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.List", "data", "POST", false);		
			request.send ()
			
			return request.respons ()["scms.roots"];	
		}		
		
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: page
	// ---------------------------------------------------------------------------------------------------------------
	page :
	{
		// Delay before executing asyncronis request.
		_asyncdelay : 10,
		
		new : function ()
		{	
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.New", "data", "POST", false);		
			request.send ();
		
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
			request.send (page);
		
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
		
		list : function ()
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Page.List", "data", "POST", false);		
			request.send ();
				
			return request.respons ()["scms.pages"];	
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
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: stylesheet
	// ---------------------------------------------------------------------------------------------------------------
	stylesheet :
	{
		// Delay before executing asyncronis request.
		_asyncdelay : 10,
		
		new : function (title)
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.New", "data", "POST", false);		
			
			var content = new Array ();
			content["title"] = title;
			
			request.send (content);
		
			return request.respons ()["scms.stylesheet"];
		},
			
		load : function (id)
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.Load", "data", "POST", false);	
		
			var content = new Array ();
			content["id"] = id;
		
			request.send (content);
		
			return request.respons ()["scms.stylesheet"];
		},
		
		save : function (stylesheet)
		{					
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.Save", "data", "POST", false);		
			
			var content = new Array ();
			content["scms.stylesheet"] = stylesheet;
			
			request.send (content);
							
			return true;
		},
		
		delete : function (id)
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.Delete", "data", "POST", false);	
		
			var content = new Array ();
			content["id"] = id;
		
			request.send (content);
							
			return true;					
		},
		
		list : function ()
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.List", "data", "POST", false);		
			request.send ();
		
			return request.respons ()["scms.stylesheets"];
		}
	}
}

