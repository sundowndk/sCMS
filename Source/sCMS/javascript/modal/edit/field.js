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
																							
								options.mediatransformation.set ();
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
						
						attributes.current.options = new Array ();
						attributes.current.options["mediatransformationids"] = options.mediatransformation.get ();						
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
							
						modal.getUIElement ("button1").setAttribute ("onClick", onButton1);
						modal.getUIElement ("close").setAttribute ("onClick", dispose);	
						
						modal.getUIElement ("mediatransformations").setAttribute ("onChange", onChange);
						modal.getUIElement ("mediatransformationadd").setAttribute ("onClick", options.mediatransformation.add);
						modal.getUIElement ("mediatransformationremove").setAttribute ("onClick", options.mediatransformation.remove);
							
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
						
						if (modal.getUIElement ("type").getAttribute ("selectedItem").value == "Image")
						{							
							modal.getUIElement ("options").getPanel ("image").setAttribute ("hidden", false);
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
				if (attributes.current.options.mediatransformationids)
				{
					var ids = SNDK.string.trimEnd (attributes.current.options.mediatransformationids, ";").split (";")
					for (index in ids)
					{
						modal.getUIElement ("mediatransformations").addItem (sorentoLib.mediaTransformation.load (ids[index]));			
					}						
				}
			}
		}
	};		
												
	// INIT				
	var modal = new sConsole.modal.window ({width: "600px", height: "300px", titleBarUI: [{type: "button", attributes: {tag: "button1", label: ""}}, {type: "button", attributes: {tag: "close", label: "Close"}}], busy: true, SUIXML: "/console/xml/scms/modal/edit/field.xml", onInit: onInit});		
}

