fieldString : function (attributes)
{	
	if (!attributes)
	{
		attributes = new Array ();		
	}

	if (attributes.string == null)
	{					
		attributes.mode = "new";
		attributes.current = {};
		attributes.current.value = ""		
		
		attributes.title =  "Add new string";
		attributes.button1Label = "Add";
		attributes.button2Label = "Close";
	}
	else
	{
		attributes.mode = "edit";
		attributes.current = attributes.string;		
		
		attributes.title =  "Edit string";
		attributes.button1Label = "Apply";
		attributes.button2Label = "Close";
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
							if ((sConsole.helpers.compareItems ({array1: attributes.current, array2: get ()})))
							{
								modal.getUIElement ("button1").setAttribute ("disabled", false);
							}
							else
							{
								modal.getUIElement ("button1").setAttribute ("disabled", true);
							}	
					};		
									
	// SET	
	var set = 		function ()
					{
						modal.getUIElement ("string").setAttribute ("value", attributes.current.value);
					};
						
	// GET
	var get = 		function ()
					{
						var item = {};
						item.value = modal.getUIElement ("string").getAttribute ("value");
						
						return item;
					}						
												
	// INIT				
	var modal = new sConsole.modal.window ({SUIXML: "/console/xml/scms/modal/edit/fieldstring.xml"});
																																								
	modal.getUIElement ("string").setAttribute ("onChange", onChange);			
		
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

