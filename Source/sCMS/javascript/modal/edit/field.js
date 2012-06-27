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
								options.mediatransformation.set ();
								break;
							}
						}
						
						switch (attributes.current.type)
						{
							case "String":
							{
								options.string.set ();
								break;
							}
						}
						
						get ();
						
						checksum = sConsole.helpers.arrayChecksum (attributes.current);
					};
						
	// GET
	var get = 		function ()
					{
						attributes.current.name = modal.getUIElement ("name").getAttribute ("value");
						attributes.current.type = modal.getUIElement ("type").getAttribute ("selectedItem").value;							
												
						attributes.current.options = options.get ();
						
						switch (attributes.current.type)
						{
							case "String":
							{
								options.string.get ();
								break;
							}
						}
												
						//attributes.current.options["mediatransformationids"] = options.mediatransformation.get ();	
						
										
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
						//options.image.init ();
							
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
													
						//if ((sConsole.helpers.compareItems ({array1: attributes.current, array2: get ()})) && (modal.getUIElement ("name").getAttribute ("value") != ""))
						if ((sConsole.helpers.arrayChecksum (attributes.current) != checksum) && (modal.getUIElement ("name").getAttribute ("value") != ""))
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
						
						if (modal.getUIElement ("mediatransformations").getItem () != null)
						{
							modal.getUIElement ("mediatransformationremove").setAttribute ("disabled", false);
						}
						else
						{
							modal.getUIElement ("mediatransformationremove").setAttribute ("disabled", true);
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
				options.string.elements["default"].setAttribute ("onChange", onChange);
					
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
			}
		},
	
		mediatransformation : 
		{
			add : function ()
			{
				var onDone =	function (id)
								{
									if (id != null)
									{
										modal.getUIElement ("mediatransformations").addItem (sorentoLib.mediaTransformation.load (id));	
									}
								};
	
				sConsole.modal.chooser.mediaTransformation ({onDone: onDone});			
			},
		
			remove : function ()
			{
				modal.getUIElement ("mediatransformations").removeItem ();
			},		

			get : function ()
			{
				var result = "";
				
				var mediatransformations = modal.getUIElement ("mediatransformations").getItems ();
				
				for (index in mediatransformations)
				{
					result += mediatransformations[index].id +";";
				
				}
								
				return result;
			},
									
			set : function ()
			{
//				if (attributes.current.options.mediatransformationids)
//				{
//					var ids = SNDK.string.trimEnd (attributes.current.options.mediatransformationids, ";").split (";")
//					for (index in ids)
//					{
//						modal.getUIElement ("mediatransformations").addItem (sorentoLib.mediaTransformation.load (ids[index]));			
//					}						
//				}
			}
		}
	};		
												
	// INIT				
	var modal = new sConsole.modal.window ({dimensions: "auto", busy: true, SUIXML: "/console/xml/scms/modal/edit/field.xml", onInit: onInit});		
}

