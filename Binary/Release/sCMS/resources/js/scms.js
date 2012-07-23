// ---------------------------------------------------------------------------------------------------------------
// PROJECT: scms
// ---------------------------------------------------------------------------------------------------------------
// ---------------------------------------------------------------------------------------------------------------
// CLASS: sCMS
// ---------------------------------------------------------------------------------------------------------------
var sCMS =
{
	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: template
	// ---------------------------------------------------------------------------------------------------------------
	template :
	{
		new : function ()
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.New", "data", "POST", false);	
			request.send ();
		
			return request.respons ()["scms.template"];
		},
			
		load : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Load", "data", "POST", false);		
			request.send (content);
		
			return request.respons ()["scms.template"];
		},
				
		save : function (template)
		{	
			var content = new Array ();
			content["scms.template"] = template;
										
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Save", "data", "POST", false);	
			request.send (content);
		
			return true;
		},		
		
		delete : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Delete", "data", "POST", false);	
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
									attributes.onDone (respons["scms.templates"]);
								};
				
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.List", "data", "POST", true);
				request.onLoaded (onDone);
				request.send ();						
			}
			else
			{
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.List", "data", "POST", false);		
				request.send ();
		
				return request.respons ()["scms.templates"];		
			}
		}	
		
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: collectionSchema
	// ---------------------------------------------------------------------------------------------------------------
	collectionSchema :
	{
		new : function (title)
		{
			var content = new Array ();
			content["title"] = title;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.New", "data", "POST", false);		
			request.send (content);
		
			return request.respons ()["scms.collectionschema"];
		},		
		
		load : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Load", "data", "POST", false);	
			request.send (content);
		
			return request.respons ()["scms.collectionschema"];
		},
		
		save : function (collectionschema)
		{					
			var content = new Array ();
			content["scms.collectionschema"] = collectionschema
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Save", "data", "POST", false);						
			request.send (content);
							
			return true;
		},
		
		delete : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Delete", "data", "POST", false);	
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
									attributes.onDone (respons["scms.collectionschemas"]);
								};
								
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.List", "data", "POST", true);
				request.onLoaded (onDone);
				request.send ();
			}
			else
			{
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.List", "data", "POST", false);		
				request.send ();
		
				return request.respons ()["scms.collectionschemas"];	
			}
		}
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: collection
	// ---------------------------------------------------------------------------------------------------------------
	collection :
	{
		new : function (collectionSchemaId, Title)
		{
			var content = new Array ();
			content["collectionschemaid"] = collectionSchemaId;
			content["title"] = Title;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.New", "data", "POST", false);		
			request.send (content);
		
			return request.respons ()["scms.collection"];
		},		
		
		load : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.Load", "data", "POST", false);	
			request.send (content);
		
			return request.respons ()["scms.collection"];
		},
		
		save : function (collection)
		{					
			var content = new Array ();
			content["scms.collection"] = collection
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.Save", "data", "POST", false);						
			request.send (content);
							
			return true;
		},
		
		delete : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.Delete", "data", "POST", false);	
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
									attributes.onDone (respons["scms.collections"]);
								};
								
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.List", "data", "POST", true);
				request.onLoaded (onDone);
				request.send ();
			}
			else
			{
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.List", "data", "POST", false);		
				request.send ();
		
				return request.respons ()["scms.collections"];	
			}
		}
		
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: global
	// ---------------------------------------------------------------------------------------------------------------
	global :
	{
		new : function (type, name)
		{
			var content = new Array ();
			content["type"] = type;
			content["name"] = name;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.New", "data", "POST", false);		
			request.send (content);
		
			return request.respons ()["scms.global"];
		},		
		
		load : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Load", "data", "POST", false);	
			request.send (content);
		
			return request.respons ()["scms.global"];
		},
		
		save : function (global)
		{					
			var content = new Array ();
			content["scms.global"] = global;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Save", "data", "POST", false);						
			request.send (content);
							
			return true;
		},
		
		delete : function (id)
		{
			var content = new Array ();
			content["id"] = id;
		
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Delete", "data", "POST", false);	
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
									attributes.onDone (respons["scms.globals"]);
								};
								
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.List", "data", "POST", true);
				request.onLoaded (onDone);
				request.send ();
			}
			else
			{
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.List", "data", "POST", false);		
				request.send ();
		
				return request.respons ()["scms.globals"];	
			}
		}
	},

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
		
		list : function (attributes)
		{
			if (!attributes) attributes = new Array ();
			
			if (attributes.async)
			{
				var onDone = 	function (respons)
								{
									attributes.onDone (respons["scms.roots"]);
								};
								
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.List", "data", "POST", true);
				request.onLoaded (onDone);
				request.send ();
			}
			else
			{
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.List", "data", "POST", false);		
				request.send ();
		
				return request.respons ()["scms.roots"];	
			}
		}
		
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: page
	// ---------------------------------------------------------------------------------------------------------------
	page :
	{
		// Delay before executing asyncronis request.
		_asyncdelay : 10,
		
		new : function (rootid, templateid, parentid, title)
		{	
			var content = new Array ();
			content["rootid"] = rootid;
			content["templateid"] = templateid;
			content["parentid"] = parentid;
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
		
		list : function (attributes)
		{
			if (!attributes) attributes = new Array ();
			
			if (attributes.async)
			{
				var onDone = 	function (respons)
								{
									attributes.onDone (respons["scms.stylesheets"]);
								};
								
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.List", "data", "POST", true);
				request.onLoaded (onDone);
				request.send ();
			}
			else
			{
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.List", "data", "POST", false);		
				request.send ();
		
				return request.respons ()["scms.stylesheets"];	
			}
		}
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: javascript
	// ---------------------------------------------------------------------------------------------------------------
	javascript :
	{
		new : function (title)
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.New", "data", "POST", false);		
			
			var content = new Array ();
			content["title"] = title;
			
			request.send (content);
		
			return request.respons ()["scms.javascript"];
		},
			
		load : function (id)
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.Load", "data", "POST", false);	
		
			var content = new Array ();
			content["id"] = id;
		
			request.send (content);
		
			return request.respons ()["scms.javascript"];
		},
		
		save : function (javascript)
		{					
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.Save", "data", "POST", false);		
			
			var content = new Array ();
			content["scms.javascript"] = javascript;
			
			request.send (content);
							
			return true;
		},
		
		delete : function (id)
		{
			var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.Delete", "data", "POST", false);	
		
			var content = new Array ();
			content["id"] = id;
		
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
									attributes.onDone (respons["scms.javascripts"]);
								};
								
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.List", "data", "POST", true);
				request.onLoaded (onDone);
				request.send ();
			}
			else
			{
				var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.List", "data", "POST", false);		
				request.send ();
		
				return request.respons ()["scms.javascripts"];	
			}
		}
		
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: sui
	// ---------------------------------------------------------------------------------------------------------------
	sui :
	{
		// -------------------------------------------------------------------------------------------------------------------------
		// field ([attributes])
		// -------------------------------------------------------------------------------------------------------------------------
		//
		// Methods:
		//
		//	refresh ()
		//	getAttribute (attribute)
		//	setAttribute (attribute, value)
		//
		// Attributes:
		//		
		//	id			get
		//	tag 		get/set
		//	name 		get/set
		//	width		get/set
		//	height		get/set
		//	appendTo	get/set
		//	managed		get/set
		//	disabled	get/set
		//	focus		get/set
		//	onFocus		get/set
		//	onBlur		get/set
		//	onChange	get/set
		//	onKeyUp		get/set
		//	value		get/set
		
		/**
		 * @constructor
		 */
		field : function (attributes)
		{
			var _elements = new Array ();
			var _attributes = attributes;
			var _elements = new Array ();	
			var _temp = 	{ initialized: false,
							};
			
			// Functions		
			this.refresh = functionRefresh;	
			this.setAttribute = functionSetAttribute;
			this.getAttribute = functionGetAttribute;
		
			// Construct
			construct ();
			
			// Private functions
			this._attributes = _elements["container"]._attributes;
			this._elements = _elements["container"]._elements;
			this._temp = _elements["container"]._temp;	
			this._init = _elements["container"]._init;	
				
			// ------------------------------------
			// Private functions
			// ------------------------------------
			// ------------------------------------
			// init
			// ------------------------------------		
			function init ()
			{
				_elements["container"]._init ();
			}
			
			// ------------------------------------
			// construct
			// ------------------------------------	
			function construct ()
			{		
				_elements["container"] = new SNDK.SUI.layoutbox ({width: _attributes.width, height: _attributes.height, type: "vertical", stylesheet: "SUILayoutboxNoBorder"});		
				_elements["container"].addPanel ({tag: "containerpanel", size: "*"});
					
				switch (_attributes.type)
				{
					case "string":
					{
						_elements["content"] = new SNDK.SUI.textbox ({tag: _attributes.tag, width: "100%"});
						_elements["container"].getPanel ("containerpanel").addUIElement (_elements["content"]);		
						break;
					}		
					
					case "text":
					{
						var providerconfig = {};			
						providerconfig.theme = "advanced";
						providerconfig.plugins = "table,paste";
						providerconfig.theme_advanced_toolbar_location = "top";
						providerconfig.theme_advanced_toolbar_align = "left";
						providerconfig.theme_advanced_buttons1 = "bold,italic,underline,|,justifyleft, justifycenter, justifyright, justifyfull,|,formatselect,removeformat,|,numlist,bullist,|,undo";
						providerconfig.theme_advanced_buttons2 = "";
						providerconfig.theme_advanced_buttons3 = "";
						providerconfig.theme_advanced_blockformats = "h1,h2,h3,h4,h5,h6,blockquote";
						providerconfig.paste_auto_cleanup_on_paste = true;
						providerconfig.paste_remove_spans = true;
						providerconfig.paste_remove_styles = true;
						providerconfig.paste_remove_styles_if_webkit = true;
						providerconfig.paste_strip_class_attributes = "mso";		
					
						_elements["content"] = new SNDK.SUI.textarea ({tag: _attributes.tag, width: "100%", height: "100%", provider: "tinymce", providerConfig: providerconfig})
						_elements["container"].getPanel ("containerpanel").addUIElement (_elements["content"]);		
						break;
					}		
				}
				
			}	
				
			// ------------------------------------
			// refresh
			// ------------------------------------		
			function refresh ()
			{
				if (_attributes.disabled)
				{
					switch (_attributes.type)
					{
						case "string":
						{
							_elements["content"].setAttribute ("disabled", true);
							break;
						}
						
						case "text":
						{
							_elements["content"].setAttribute ("disabled", true);
							break;	
						}
					}		
				}
				else
				{
					switch (_attributes.type)
					{
						case "string":
						{
							_elements["content"].setAttribute ("disabled", false);
							break;
						}
						
						case "text":
						{
							_elements["content"].setAttribute ("disabled", false);
							break;					
						}
					}
				}
			}
			
			// ------------------------------------
			// Public functions
			// ------------------------------------		
			// ------------------------------------
			// refresh
			// ------------------------------------				
			function functionRefresh ()
			{		
				_elements["container"].refresh ();	
			}		
				
			// ------------------------------------
			// getAttribute
			// ------------------------------------						
			function functionGetAttribute (attribute)
			{			
				switch (attribute)
				{
					case "id":
					{
						return _elements["content"].getAttribute ("id");
					}			
		
					case "tag":
					{
						return _elements["content"].getAttribute ("tag");
					}			
					
					case "width":
					{
						return _elements["container"].getAttribute ("width")
					}
					
					case "height":
					{
						return _elements["container"].getAttribute ("height")
					}	
					
					case "appendTo":
					{
						return _elements["container"].getAttribute ("appendTo");
					}			
					
					case "managed":
					{
						return _elements["container"].getAttribute ("managed");
					}
					
					case "focus":
					{
						return _elements["content"].getAttribute ("focus");
					}
		
					case "onFocus":
					{
						return _elements["content"].getAttribute ("onFocus");
					}
		
					case "onBlur":
					{
						return _elements["content"].getAttribute ("onBlur");
					}
		
					case "onChange":
					{
						return _elements["content"].getAttribute ("onChange");
					}
		
					case "onKeyUp":
					{
						return _elements["content"].getAttribute ("onKeyUp");
					}
		
					case "value":
					{
						return _elements["content"].getAttribute ("value");
					}			
		
					default:
					{
						throw "No attribute with the name '"+ attribute +"' exist in this object";
					}
				}
			}
			
			// ------------------------------------
			// setAttribute
			// ------------------------------------						
			function functionSetAttribute (attribute, value)
			{
				switch (attribute)
				{
					case "id":
					{
						throw "Attribute with name ID is ready only.";
						break;
					}
					
					case "tag":
					{
						_elements["content"].setAttribute ("tag", value);				
						break;
					}
					
					case "name":
					{
						_elements["content"].setAttribute ("name", value);				
						break;
					}
								
					case "width":
					{
						_elements["container"].setAttribute ("width", value);
						break;			
					}
		
					case "height":
					{
						_elements["container"].setAttribute ("height", value);
						break;			
					}
					
					case "appendTo":
					{
						_elements["container"].setAttribute ("appendTo", value);
						break;
					}			
					
					case "managed":
					{
						_elements["container"].setAttribute ("managed", value);
						break;
					}
					
					case "disabled":
					{
						_attributes[attribute] = value;
						refresh ();
						break;
					}
					
					case "focus":
					{
						_elements["content"].setAttribute ("focus", value);
						break;
					}
		
					case "onFocus":
					{
						_elements["content"].setAttribute ("onFocus", value);
						break;
					}
		
					case "onBlur":
					{
						_elements["content"].setAttribute ("onBlur", value);
						break;
					}
		
					case "onChange":
					{
						_elements["content"].setAttribute ("onChange", value);
						break;
					}
		
					case "onKeyUp":
					{
						_elements["content"].setAttribute ("onKeyUp", value);
						break;
					}
		
					case "value":
					{
						_elements["content"].setAttribute ("value", value);
						break;
					}			
							
					default:
					{
						throw "No attribute with the name '"+ attribute +"' exist in this object";
					}
				}	
			}										
							
			// ------------------------------------
			// Events
			// ------------------------------------
		}
	},

	// ---------------------------------------------------------------------------------------------------------------
	// CLASS: modal
	// ---------------------------------------------------------------------------------------------------------------
	modal :
	{
		// ---------------------------------------------------------------------------------------------------------------
		// CLASS: tinymce
		// ---------------------------------------------------------------------------------------------------------------
		tinymce :
		{
			link : function (attributes)
			{					
				if (!attributes) attributes = new Array ();
								
				var choosepage =	function ()
									{
										var onDone =	function (result)
														{							
															if (result != null)
															{																					
																var page = sCMS.page.load (result.id);													
																modal.getUIElement ("link").setAttribute ("value", page.path);													
															}
														};
				
										sCMS.modal.chooser.page ({onDone: onDone});								
									};									
								
				var onDone =	function ()
								{
									attributes.onDone (get ());
									modal.dispose ();
								};
								
				var onChange =	function ()
								{
									if (modal.getUIElement ("link").getAttribute ("value") != "")
									{
										modal.getUIElement ("apply").setAttribute ("disabled", false);
									}
									else
									{
										modal.getUIElement ("apply").setAttribute ("disabled", true);
									}
								}					
									
				var set = 		function ()
								{
									modal.getUIElement ("link").setAttribute ("value", attributes.value);
								};
								
				var get =		function ()
								{
									var result = modal.getUIElement ("link").getAttribute ("value");
									return result;
								};
								
				var suixml = "";
				suixml += '<sui elementheight="50px">';
				suixml += '	<canvas width="600px" height="180px" canScroll="false">';
				suixml += '		<container title="Insert link" icon="Icon32Edit" stylesheet="SUIContainerModal">';
				suixml += '			<layoutbox type="horizontal" stylesheet="LayoutboxNoborder">';
				suixml += '				<panel size="%elementheight%">';
				suixml += '					<layoutbox type="vertical">';
				suixml += '						<panel size="100px">';
				suixml += '							<label text="Link"/>';
				suixml += '						</panel>';		
				suixml += '						<panel size="*">';
				suixml += '							<textbox tag="link" width="80%" focus="true" />';
				suixml += '							<button tag="choosepage" label="choose" width="20%" />';
				suixml += '						</panel>';				
				suixml += '					</layoutbox>';
				suixml += '				</panel>';
				suixml += '				<panel size="*">';
				suixml += '				</panel>';
				suixml += '				<panel size="45px">';
				suixml += '					<layoutbox type="vertical">';
				suixml += '						<panel size="*">';
				suixml += '						</panel>';
				suixml += '						<panel size="210px">';
				suixml += '							<button tag="apply" label="Apply" width="100px" disabled="true"/>';
				suixml += '							<button tag="close" label="Close" width="100px" />';
				suixml += '						</panel>';					
				suixml += '					</layoutbox>';
				suixml += '				</panel>';
				suixml += '			</layoutbox>';
				suixml += '		</container>';
				suixml += '	</canvas>';
				suixml += '</sui>';
																		
				var modal = new sConsole.modal.window ({XML: suixml});
								
				// LINK
			//	elements["linkcontainer"] = modalwindow.addControl ({type: "empty", label: "Link :"});			
			//	elements["link"] = new SNDK.SUI.textbox ({appendTo: elements["linkcontainer"], width: "501px", value: options.value, focus: true, onKeyUp: null});
			//	elements["linkbutton"] = new SNDK.SUI.button ({appendTo: elements["linkcontainer"], label: "&raquo;", width: "30px", onClick: choosepage});
			
						// TARGET
			//			elements["target"] = modalwindow.addControl ({type: "dropbox", label: "Target :"});
																									
				// BUTTONS
			//	elements["add"] = modalwindow.addButton ({label: "Apply", disabled: false, onClick: done});
			//	elements["close"] = modalwindow.addButton ({label: "Close", onClick: modalwindow.dispose});
			
				modal.getUIElement ("apply").setAttribute ("onClick", onDone);
				modal.getUIElement ("close").setAttribute ("onClick", modal.dispose);
				
				modal.getUIElement ("link").setAttribute ("onChange", onChange);
				modal.getUIElement ("choosepage").setAttribute ("onClick", choosepage);
						
				// SET		
				set ();
						
				// SHOW		
				modal.show ();
			}	
		},
	
		// ---------------------------------------------------------------------------------------------------------------
		// CLASS: edit
		// ---------------------------------------------------------------------------------------------------------------
		edit :
		{
			field : function (attributes)
			{			
				// SET	
				var set = 		function ()
								{
									modal.getUIElement ("name").setAttribute ("value", attributes.current.name);
									modal.getUIElement ("type").setAttribute ("selectedItemByValue", attributes.current.type);
								
									switch (attributes.mode)
									{
										case "edit":
										{
											modal.getUIElement ("type").setAttribute ("disabled", true);
																				
											options.set ();														
											break;
										}
									}
									
									switch (attributes.current.type.toLowerCase ())
									{
										case "string":
										{
											options.string.set ();
											break;
										}
										
										case "image":
										{
											options.image.set ();
											break;
										}
									}
									
									get ();
									
									attributes.checksum = sConsole.helpers.arrayChecksum (attributes.current);
								};
									
				// GET
				var get = 		function ()
								{
									attributes.current.name = modal.getUIElement ("name").getAttribute ("value");
									attributes.current.type = modal.getUIElement ("type").getAttribute ("selectedItem").value;							
															
									attributes.current.options = options.get ();
									
									switch (attributes.current.type.toLowerCase ())
									{
										case "string":
										{
											options.string.get ();
											break;
										}
										
										case "image":
										{
											options.image.get ();
											break;
										}
									}
								}			
																			
				// DISPOSE
				var dispose =	function ()
								{
									modal.dispose ();
								};
					
				// ONINIT
				var onInit =	function ()
								{
									if (!attributes)
										attributes = new Array ();
				
									if (attributes.field == null)
									{					
										attributes.mode = "new";
										attributes.current = {};
										attributes.current.id = SNDK.tools.newGuid ();
										attributes.current.type = "String";
										attributes.current.name = "";
										attributes.current.options = new Array ();
										
										attributes.title = "Add Field";
										attributes.button1Label = "Add";							
									}
									else
									{
										attributes.mode = "edit";
										attributes.current = attributes.field;		
										
										attributes.title = "Edit Field";
										attributes.button1Label = "Apply";
									}	
										
									modal.getUIElement ("name").setAttribute ("onChange", onChange);			
									modal.getUIElement ("type").setAttribute ("onChange", onChange);	
										
									modal.getUIElement ("button1").setAttribute ("onClick", onButton1);
									modal.getUIElement ("close").setAttribute ("onClick", dispose);	
									
									modal.getUIElement ("hidden").setAttribute ("onChange", onChange);						
																											
									modal.getUIElement ("container").setAttribute ("title", attributes.title);
									modal.getUIElement ("button1").setAttribute ("label", attributes.button1Label);			
									
									options.string.init ();
									options.image.init ();
										
									SNDK.SUI.redraw ();												
												
									// SET
									set ();						
										
									// SHOW
									modal.show ();																				
								};			
								
				// ONBUTTON1					
				var onButton1 =	function ()
								{										
									dispose ();
															
									if (attributes.onDone != null)
									{	
										get ();							
										setTimeout (function () {attributes.onDone (attributes.current)}, 1);
									}						
								};																					
																																
				// ONCHANGE
				var onChange =	function ()
								{														
									get ();				    	
																						
									if ((sConsole.helpers.arrayChecksum (attributes.current) != attributes.checksum) && (modal.getUIElement ("name").getAttribute ("value") != ""))
									{
										modal.getUIElement ("button1").setAttribute ("disabled", false);
									}
									else
									{
										modal.getUIElement ("button1").setAttribute ("disabled", true);
									}	
										
									if (modal.getUIElement ("type").getAttribute ("selectedItem").value == "String")
									{													
										modal.getUIElement ("options").getPanel ("string").setAttribute ("hidden", false);
									}
									else
									{
										modal.getUIElement ("options").getPanel ("string").setAttribute ("hidden", true);
									}																		
																																											
									if (modal.getUIElement ("type").getAttribute ("selectedItem").value == "Image")
									{													
										modal.getUIElement ("options").getPanel ("image").setAttribute ("hidden", false);
									}
									else
									{
										modal.getUIElement ("options").getPanel ("image").setAttribute ("hidden", true);
									}						
								};		
												
								
				var test = function (array, key, value)
				{		
					var count = 0;
					for (index in array)
					{
						for (index2 in array[index])
						{
							if (index2 == key)
							{
								return count
							}
						}		
						count++;
					}			
					return -1
				}
								
				var options =	
				{
					set : function ()
					{
						try
						{
							if (attributes.current.options.hidden)
							{
								modal.getUIElement ("hidden").setAttribute ("value", true);									
							}
						}
						catch (error)
						{				
						}
					},
					
					get : function ()
					{
						var result = new Array ();
						
						result["hidden"] = modal.getUIElement ("hidden").getAttribute ("value");
									
						return result;
					},
				
					string : 
					{
						elements : new Array (),
					
						init : function ()
						{
							options.string.elements["default"] = new SNDK.SUI.field ({type: "string", options: {}, width: "100%", height: "100%"});	
							options.string.elements["default"].setAttribute ("onChange", options.string.onChange);
								
							modal.getUIElement ("stringdefaultbox").getPanel ("stringdefaultpanel").addUIElement (options.string.elements["default"]);
						},
						
						get : function ()
						{
							attributes.current.options.default = options.string.elements["default"].getAttribute ("value");
						
						},
						
						set : function ()
						{
							if (attributes.current.options.default)
							{
								options.string.elements["default"].setAttribute ("value", attributes.current.options.default);
							}
						},
						
						onChange : function ()
						{
							onChange ();
						}
					},
				
					image : 
					{
						elements : new Array (),
					
						init : function ()
						{		
							options.image.elements["layoutbox"] = new SNDK.SUI.layoutbox ({type: "vertical", stylesheet: "LayoutboxNoborder"})
							
							var panel1 = options.image.elements["layoutbox"].addPanel ({size: "*"});
							var panel2 = options.image.elements["layoutbox"].addPanel ({size: "80px"});
							
							var columns = new Array ();
							columns[0] = {tag: "id"};
							columns[1] = {tag: "title", label: "Title", width: "150px", visible: true};
							
							options.image.elements["mediatransformations"] = new SNDK.SUI.listview ({columns: columns, width: "100%", height: "100%"})				
							options.image.elements["mediatransformations"].setAttribute ("onChange", options.image.onChange);				
							panel1.addUIElement (options.image.elements["mediatransformations"]);
							
							options.image.elements["mediatransformationadd"] = new SNDK.SUI.button ({label: "Add", width: "100%"});
							options.image.elements["mediatransformationadd"].setAttribute ("onClick", options.image.mediatransformationAdd);
							options.image.elements["mediatransformationremove"] = new SNDK.SUI.button ({label: "Remove", width: "100%"});
							options.image.elements["mediatransformationremove"].setAttribute ("onClick", options.image.mediatransformationRemove);
							
							panel2.addUIElement (options.image.elements["mediatransformationadd"]);
							panel2.addUIElement (options.image.elements["mediatransformationremove"]);
																	
							modal.getUIElement ("imagemediatransformationbox").getPanel ("imagemediatransformationpanel").addUIElement (options.image.elements["layoutbox"]);				
						},		
					
						mediatransformationAdd : function ()
						{
							var onDone =	function (id)
											{
												if (id != null)
												{
													options.image.elements["mediatransformations"].addItem (sorentoLib.mediaTransformation.load (id))									
												}
											};
				
							sConsole.modal.chooser.mediaTransformation ({onDone: onDone});			
						},
					
						mediatransformationRemove : function ()
						{
							options.image.elements["mediatransformations"].removeItem ();				
						},		
			
						get : function ()
						{
							var result = "";
							
							var mediatransformations = options.image.elements["mediatransformations"].getItems ();
							
							for (index in mediatransformations)
							{
								result += mediatransformations[index].id +";";				
							}
											
							attributes.current.options.mediatransformations = result;
						},
												
						set : function ()
						{
							if (attributes.current.options.mediatransformations)
							{
								var ids = SNDK.string.trimEnd (attributes.current.options.mediatransformations, ";").split (";")
								for (index in ids)
								{
									options.image.elements["mediatransformations"].addItem (sorentoLib.mediaTransformation.load (ids[index]));
								}						
							}
							
							options.image.onChange ();
						},
						
						onChange : function ()
						{
							onChange ();								
						
							if (options.image.elements["mediatransformations"].getItem () != null)
							{					
								options.image.elements["mediatransformationremove"].setAttribute ("disabled", false);					
							}
							else
							{					
								options.image.elements["mediatransformationremove"].setAttribute ("disabled", true);
							}
						}
					}
				};		
															
				// INIT				
				var modal = new sConsole.modal.window ({dimensions: "auto", busy: true, SUIXML: "/console/xml/scms/modal/edit/field.xml", onInit: onInit});		
			}
			,
		
			fieldString : function (attributes)
			{	
				// SET	
				var set = 		function ()
								{
									modal.getUIElement ("string").setAttribute ("value", attributes.current.value);
									
									attributes.checksum = sConsole.helpers.arrayChecksum (attributes.current);
									
									onChange ();
								};
									
				// GET
				var get = 		function ()
								{
									attributes.current.value = modal.getUIElement ("string").getAttribute ("value");
								};
								
				// DISPOSE
				var dispose =	function ()
								{
									modal.dispose ();
								};
			
				// ONINIT
				var onInit =	function ()
								{
									if (!attributes)
										attributes = new Array ();		
			
									if (attributes.string == null)
									{					
										attributes.mode = "new";
										attributes.current = {};
										attributes.current.value = ""		
										
										attributes.title =  "Add new string";
										attributes.button1Label = "Add";							
									}
									else
									{
										attributes.mode = "edit";
										attributes.current = attributes.string;		
										
										attributes.title =  "Edit string";
										attributes.button1Label = "Apply";
									}
									
									modal.getUIElement ("string").setAttribute ("onChange", onChange);			
					
									modal.getUIElement ("button1").setAttribute ("onClick", onButton1);
									modal.getUIElement ("close").setAttribute ("onClick", dispose);	
					
									modal.getUIElement ("container").setAttribute ("title", attributes.title);
									modal.getUIElement ("button1").setAttribute ("label", attributes.button1Label);
							
									// SET
									set ();						
						
									// SHOW
									modal.show ();	
								};														
																																													
				// ONBUTTON1
				var onButton1 =	function ()
								{										
									get ();
								
									dispose ();						
															
									if (attributes.onDone != null)
									{	
										setTimeout (function () {attributes.onDone (attributes.current)}, 1);
									}						
								};
								
				// ONCHANGE
				var onChange =	function ()
								{		
									get ();
									
									if (sConsole.helpers.arrayChecksum (attributes.current) != attributes.checksum)		
									{
										modal.getUIElement ("button1").setAttribute ("disabled", false);
									}
									else
									{
										modal.getUIElement ("button1").setAttribute ("disabled", true);
									}	
								};		
																													
				// INIT				
				var modal = new sConsole.modal.window ({width: "600px", height: "150px", titleBarUI: [{type: "button", attributes: {tag: "button1", label: ""}}, {type: "button", attributes: {tag: "close", label: "Close"}}], busy: true, SUIXML: "/console/xml/scms/modal/edit/fieldstring.xml", onInit: onInit});
			}
			,
		
			fieldRender : function (attributes)
			{	
				var result = new Array ();
			
				for (i=0; i < attributes.fields.length; i++)			
				{
					if (!attributes.fields[i].options.hidden)
					{			
						var height;			
				
						switch (attributes.fields[i].type.toLowerCase ())
						{
							case "string":
							{
								height = "50px";
								break;
							}
						
							case "link":
							{
								height = "50px";
								break;
							}
						
							case "text":
							{
								height = "300px";
								break;
							}
						
							default:
							{
								height = "220px";
								break;				
							}
						}
																					
						var layoutbox = new SNDK.SUI.layoutbox ({type: "vertical", height: height});
						layoutbox.addPanel ({tag: "text", size: "100px"});
						layoutbox.addPanel ({tag: "field", size: "*"});			
						layoutbox.getPanel ("text").addUIElement (new SNDK.SUI.label ({text: attributes.fields[i].name}));
																						
						result[attributes.fields[i].id] = new SNDK.SUI.field ({type: attributes.fields[i].type.toLowerCase (), options: attributes.fields[i].options, width: "100%", height: "100%", onChange: attributes.onChange});
					
						layoutbox.getPanel ("field").addUIElement (result[attributes.fields[i].id]);
							
						attributes.appendTo.addUIElement (layoutbox);			
					}
				}		
				
				return result;
			}
		},
	
		// ---------------------------------------------------------------------------------------------------------------
		// CLASS: chooser
		// ---------------------------------------------------------------------------------------------------------------
		chooser :
		{
			stylesheet : function (attributes)
			{
				// SET			
				var set =		function ()
								{
									chooser.getUIElement ("stylesheets").setItems (sCMS.stylesheet.list ());						
								};
			
				// ONINIT					
				var onInit =	function ()
								{				
									chooser.getUIElement ("stylesheets").setAttribute ("onChange", onChange);
							
									// SET
									set ();
							
									// SHOW
									chooser.show ();							
								};
			
				// ONBUTTON1
				var onButton1 =	function ()
								{
									chooser.dispose ();
									
									if (attributes.onDone != null)
									{
										setTimeout( function ()	{ attributes.onDone (chooser.getUIElement ("stylesheets").getItem ()); }, 1);
									}
								};
							
				// ONBUTTON2	
				var onButton2 =	function ()
								{
									chooser.dispose ();
									
									if (attributes.onDone != null)
									{
										setTimeout( function ()	{ attributes.onDone (null); }, 1);
									}						
								};
								
				// ONCHANGE	
				var onChange = 	function ()
								{
									if (chooser.getUIElement ("stylesheets").getItem ())
									{
										chooser.getUIElement ("button1").setAttribute ("disabled", false);
									}
									else
									{
										chooser.getUIElement ("button1").setAttribute ("disabled", true);
									}
								};											
			
				var suixml = "";
				suixml += '<sui>';
			//	suixml += '	<layoutbox type="horizontal">';
			//	suixml += '		<panel size="*">';
			//	suixml += '			<layoutbox type="vertical">';
			//	suixml += '				<panel size="*">';
				suixml += '					<listview tag="stylesheets" width="100%" height="100%" focus="true">';
				suixml += '						<column tag="id" />';
				suixml += '						<column tag="title" label="Title" width="200px" visible="true" />';	
				suixml += '					</listview>';
			//	suixml += '				</panel>';
			//	suixml += '			</layoutbox>';
			//	suixml += '		</panel>';
			//	suixml += '	</layoutbox>';
				suixml += '</sui>';
				
				var chooser = new sConsole.modal.chooser.base ({suiXML: suixml, title: "Choose stylesheet", button1Label: "Select", button2Label: "Close", onClickButton1: onButton1, onClickButton2: onButton2, onInit: onInit});
			}	
			
			
			,
		
			javascript : function (attributes)
			{
				// SET			
				var set =		function ()
								{
									chooser.getUIElement ("javascripts").setItems (sCMS.javascript.list ());
								};
			
				// ONINIT					
				var onInit =	function ()
								{				
									chooser.getUIElement ("javascripts").setAttribute ("onChange", onChange);
							
									// SET
									set ();
							
									// SHOW
									chooser.show ();							
								};
			
				// ONBUTTON1
				var onButton1 =	function ()
								{
									chooser.dispose ();
									
									if (attributes.onDone != null)
									{
										setTimeout( function ()	{ attributes.onDone (chooser.getUIElement ("javascripts").getItem ()); }, 1);
									}
								};
							
				// ONBUTTON2	
				var onButton2 =	function ()
								{
									chooser.dispose ();
									
									if (attributes.onDone != null)
									{
										setTimeout( function ()	{ attributes.onDone (null); }, 1);
									}						
								};
								
				// ONCHANGE	
				var onChange = 	function ()
								{
									if (chooser.getUIElement ("javascripts").getItem ())
									{
										chooser.getUIElement ("button1").setAttribute ("disabled", false);
									}
									else
									{
										chooser.getUIElement ("button1").setAttribute ("disabled", true);
									}
								};					
			
				var suixml = "";
				suixml += '<sui>';
			//	suixml += '	<layoutbox type="horizontal">';
			//	suixml += '		<panel size="*">';
			//	suixml += '			<layoutbox type="vertical">';
			//	suixml += '				<panel size="*">';
				suixml += '					<listview tag="javascripts" width="100%" height="100%" focus="true">';
				suixml += '						<column tag="id" />';
				suixml += '						<column tag="title" label="Title" width="200px" visible="true" />';	
				suixml += '					</listview>';
			//	suixml += '				</panel>';
			//	suixml += '			</layoutbox>';
			//	suixml += '		</panel>';
			//	suixml += '	</layoutbox>';
				suixml += '</sui>';
				
				var chooser = new sConsole.modal.chooser.base ({suiXML: suixml, title: "Choose javascript", button1Label: "Select", button2Label: "Close", onClickButton1: onButton1, onClickButton2: onButton2, onInit: onInit});	
			}	
			
			
			
			,
		
			template : function (attributes)
			{
				// SET			
				var set =		function ()
								{
									chooser.getUIElement ("templates").setItems (sCMS.template.list ());										
								};
			
				// ONINIT					
				var onInit =	function ()
								{				
									chooser.getUIElement ("templates").setAttribute ("onChange", onChange);
							
									// SET
									set ();
							
									// SHOW
									chooser.show ();							
								};
			
				// ONBUTTON1
				var onButton1 =	function ()
								{
									chooser.dispose ();
									
									if (attributes.onDone != null)
									{
										setTimeout( function ()	{ attributes.onDone (chooser.getUIElement ("templates").getItem ()); }, 1);
									}
								};
							
				// ONBUTTON2	
				var onButton2 =	function ()
								{
									chooser.dispose ();
									
									if (attributes.onDone != null)
									{
										setTimeout( function ()	{ attributes.onDone (null); }, 1);
									}						
								};
								
				// ONCHANGE	
				var onChange = 	function ()
								{
									if (chooser.getUIElement ("templates").getItem ())
									{
										chooser.getUIElement ("button1").setAttribute ("disabled", false);
									}
									else
									{
										chooser.getUIElement ("button1").setAttribute ("disabled", true);
									}
								};					
								
				var suixml = "";
				suixml += '<sui>';
			//	suixml += '	<layoutbox type="horizontal">';
			//	suixml += '		<panel size="*">';
			//	suixml += '			<layoutbox type="vertical">';
			//	suixml += '				<panel size="*">';
				suixml += '					<listview tag="templates" width="100%" height="100%" focus="true" treeview="true" treeviewLinkColumns="id:parentid" treeviewRootValue="00000000-0000-0000-0000-000000000000">';
				suixml += '						<column tag="id" />';
				suixml += '						<column tag="title" label="Title" width="200px" visible="true" />';	
				suixml += '						<column tag="parentid" />'
				suixml += '					</listview>';
			//	suixml += '				</panel>';
			//	suixml += '			</layoutbox>';
			//	suixml += '		</panel>';
			//	suixml += '	</layoutbox>';
				suixml += '</sui>';
			
				var chooser = new sConsole.modal.chooser.base ({suiXML: suixml, title: "Choose template", button1Label: "Select", button2Label: "Close", onClickButton1: onButton1, onClickButton2: onButton2, onInit: onInit});
			}
			
			
			
			
			,
		
			page : function (attributes)
			{
				// SET			
				var set =		function ()
								{
									var roots =	function (result)
												{						
													for (index in result)
													{
														result[index].type = "root";
													}							
													chooser.getUIElement ("pages").addItems (result);			
												};
																						
									var pages =	function (result)
												{					
													for (index in result)
													{
														result[index].type = "page";
														if (result[index].parentid == "00000000-0000-0000-0000-000000000000")
														{
															result[index].parentid = result[index].rootid;
														}
													}	
													chooser.getUIElement ("pages").addItems (result);
												};						
								
									sCMS.root.list ({async: true, onDone: roots});
									sCMS.page.list ({async: true, onDone: pages});
								};
			
				// ONINIT					
				var onInit =	function ()
								{				
									chooser.getUIElement ("pages").setAttribute ("onChange", onChange);
							
									// SET
									set ();
							
									// SHOW
									chooser.show ();							
								};
			
				// ONBUTTON1
				var onButton1 =	function ()
								{
									chooser.dispose ();
									
									if (attributes.onDone != null)
									{
										setTimeout( function ()	{ attributes.onDone (chooser.getUIElement ("pages").getItem ()); }, 1);
									}
								};
							
				// ONBUTTON2	
				var onButton2 =	function ()
								{
									chooser.dispose ();
									
									if (attributes.onDone != null)
									{
										setTimeout( function ()	{ attributes.onDone (null); }, 1);
									}						
								};
								
				// ONCHANGE	
				var onChange = 	function ()
								{
									if ((chooser.getUIElement ("pages").getItem ()) && (chooser.getUIElement ("pages").getItem ().type == "page"))
									{
										chooser.getUIElement ("button1").setAttribute ("disabled", false);
									}
									else
									{
										chooser.getUIElement ("button1").setAttribute ("disabled", true);
									}
								};		
								
																														
			
				var suixml = "";
				suixml += '<sui>';
			//	suixml += '	<layoutbox type="horizontal">';
			//	suixml += '		<panel size="*">';
			//	suixml += '			<layoutbox type="vertical">';
			//	suixml += '				<panel size="*">';
				suixml += '					<listview tag="pages" width="100%" height="100%" treeview="true" treeviewLinkColumns="id:parentid" treeviewRootValue="00000000-0000-0000-0000-000000000000">';
				suixml += ' 					<column tag="id" />';
				suixml += ' 					<column tag="title" label="Title" width="200px" visible="true" />';
				suixml += ' 					<column tag="type" />'
				suixml += ' 					<column tag="parentid" />'
				suixml += '					</listview>';
			//	suixml += '				</panel>';
			//	suixml += '			</layoutbox>';
			//	suixml += '		</panel>';
			//	suixml += '	</layoutbox>';
				suixml += '</sui>';
				
				var chooser = new sConsole.modal.chooser.base ({suiXML: suixml, title: "Choose stylesheet", button1Label: "Select", button2Label: "Close", onClickButton1: onButton1, onClickButton2: onButton2, onInit: onInit});
			}	
			
			
			
		}
	}
}

