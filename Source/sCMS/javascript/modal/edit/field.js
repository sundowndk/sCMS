field : function (attributes)
{	
	if (!attributes)
	{
		attributes = new Array ();
	}

	if (attributes.field == null)
	{					
		attributes.mode = "new";
		attributes.current = {};
		attributes.current.id = SNDK.tools.newGuid ();
		attributes.current.type = "String";
		attributes.current.name = "";
	}
	else
	{
		attributes.mode = "edit";
		attributes.current = attributes.field;		
	}
										
	var onCancel =	function ()
					{
						modal.dispose ();
					};
					
	var onDone =	function ()
					{										
						modal.dispose ();
												
						if (attributes.onDone != null)
						{	
							setTimeout (function () {attributes.onDone (get ())}, 1);
						}						
					};
					
	// ONCHANGE
	var onChange =	function ()
					{		
							if ((sConsole.helpers.compareItems ({array1: attributes.current, array2: get ()})) && (modal.getUIElement ("name").getAttribute ("value") != ""))
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
					};		
									
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
								break;
							}
						}
					};
						
	// GET
	var get = 		function ()
					{
						var item = {};
						item.id = attributes.current.id;																									
						item.name = modal.getUIElement ("name").getAttribute ("value");
						item.type = modal.getUIElement ("type").getAttribute ("selectedItem").value;	
						
						return item;
					}						
												
	// INIT				
	var modal = new sConsole.modal.window ({SUIXML: "/console/xml/scms/modal/edit/field.xml"});
																																								
	modal.getUIElement ("name").setAttribute ("onChange", onChange);			
		
	modal.getUIElement ("button1").setAttribute ("onClick", onDone);
	modal.getUIElement ("button2").setAttribute ("onClick", onCancel);	
		
	modal.getUIElement ("container").setAttribute ("title", attributes.title);
	modal.getUIElement ("button1").setAttribute ("label", attributes.button1Label);
	modal.getUIElement ("button2").setAttribute ("label", attributes.button2Label);
				
	// SET
	set ();						
		
	// SHOW
	modal.show ();	
}

